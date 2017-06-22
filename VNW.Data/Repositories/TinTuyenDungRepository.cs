using System;
using System.Collections.Generic;
using System.Linq;
using VNW.Data.Infrastructure;
using VNW.Data.Models;
using VNW.Data.ViewModel;

namespace VNW.Data.Repositories
{
    public interface ITinTuyenDungRepository : IRepository<TinTuyenDung>
    {
        IEnumerable<int> GetIdTinByLuong(IEnumerable<int> tinTuyenDung, string luong);

        IEnumerable<int> GetIdTinByCapBac(IEnumerable<int> tinTuyenDung, int id);

        IEnumerable<int> GetIdTinByKyNang(IEnumerable<int> tinTuyenDung, int id);

        IEnumerable<int> GetIdTinByNganhNghe(IEnumerable<int> tinTuyenDung, int id);

        IEnumerable<int> GetIdTinByTinh(IEnumerable<int> tinTuyenDung, int id);

        IEnumerable<KyNangVm> GetKyNang(IEnumerable<int> tinTuyenDung);

        IEnumerable<TinhVm> GetTinh(IEnumerable<int> tinTuyenDung);

        IEnumerable<NganhNgheVm> GetNganhNghe(IEnumerable<int> tinTuyenDung);

        IEnumerable<CapBacVm> GetCapBac(IEnumerable<int> tinTuyenDung);

        IEnumerable<int> GetListBeginTin(string keyword, string industry, string location);

        IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location, string sort,
            string nganhnghe, string diadiem, string kynang, string capbac, string mucluong);

        IEnumerable<KyNangVm> GetKyNangByTinId(int id);

        IEnumerable<PhucLoiVm> GetPhucLoiByTinId(int id);

        IEnumerable<NganhNgheVm> GetNganhNgheByTinId(int id);

        IEnumerable<TinhVm> GetTinhByTinId(int id);
    }

    public class TinTuyenDungRepository : RepositoryBase<TinTuyenDung>, ITinTuyenDungRepository
    {
        public TinTuyenDungRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<KyNangVm> GetKyNang(IEnumerable<int> tinTuyenDung)
        {
            var query = (from kn in DbContext.KyNangs
                         join ttdkn in DbContext.TinTuyenDungKyNangs
                         on kn.Id equals ttdkn.KyNangId
                         join ttd in tinTuyenDung
                         on ttdkn.TinTuyenDungId equals ttd
                         where kn.Status
                         group kn by new
                         {
                             kn.Id,
                             kn.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new KyNangVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<int> GetListBeginTin(string keyword, string industry, string location)
        {
            var queryTTD = from ttd in DbContext.TinTuyenDungs
                           where ttd.ChucDanh.Contains(keyword) && ttd.Status
                           select ttd.Id;

            int nganhNgheId = int.Parse(industry.Substring(industry.LastIndexOf('-') + 1));

            IEnumerable<int> queryNganhNghe = queryTTD;

            if (nganhNgheId != 0)
            {
                queryNganhNghe = GetIdTinByNganhNghe(queryNganhNghe, nganhNgheId);
            }

            int tinhId = int.Parse(location.Substring(location.LastIndexOf('-') + 1));

            IEnumerable<int> queryTinh = queryNganhNghe;

            if (tinhId != 0)
            {
                queryTinh = GetIdTinByTinh(queryTinh, tinhId);
            }

            return queryTinh;
        }

        public IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location,
            string sort, string nganhnghe, string diadiem, string kynang, string capbac, string mucluong)
        {
            var queryTTD = GetListBeginTin(keyword, industry, location);

            IEnumerable<int> queryNganhNghe = Enumerable.Empty<int>();

            if (!string.IsNullOrEmpty(nganhnghe))
            {
                var typeNganhNghe = nganhnghe.Split(',');
                foreach (var item in typeNganhNghe)
                {
                    queryNganhNghe = queryNganhNghe.Concat(this.GetIdTinByNganhNghe(queryTTD, int.Parse(item)));
                }
            }
            else
            {
                queryNganhNghe = queryNganhNghe.Concat(queryTTD);
            }

            IEnumerable<int> queryDiaDiem = Enumerable.Empty<int>();

            if (!string.IsNullOrEmpty(diadiem))
            {
                var typeDiaDiem = diadiem.Split(',');
                foreach (var item in typeDiaDiem)
                {
                    queryDiaDiem = queryDiaDiem.Concat(this.GetIdTinByTinh(queryNganhNghe, int.Parse(item)));
                }
            }
            else
            {
                queryDiaDiem = queryDiaDiem.Concat(queryNganhNghe);
            }

            IEnumerable<int> queryKyNang = Enumerable.Empty<int>();

            if (!string.IsNullOrEmpty(kynang))
            {
                var typeKyNang = kynang.Split(',');
                foreach (var item in typeKyNang)
                {
                    queryKyNang = queryKyNang.Concat(this.GetIdTinByKyNang(queryDiaDiem, int.Parse(item)));
                }
            }
            else
            {
                queryKyNang = queryKyNang.Concat(queryDiaDiem);
            }

            IEnumerable<int> queryCapBac = Enumerable.Empty<int>();

            if (!string.IsNullOrEmpty(capbac))
            {
                var typeCapbac = capbac.Split(',');
                foreach (var item in typeCapbac)
                {
                    queryCapBac = queryCapBac.Concat(this.GetIdTinByCapBac(queryKyNang, int.Parse(item)));
                }
            }
            else
            {
                queryCapBac = queryCapBac.Concat(queryKyNang);
            }

            IEnumerable<int> priceResult = Enumerable.Empty<int>();

            if (!string.IsNullOrEmpty(mucluong))
            {
                priceResult = priceResult.Concat(this.GetIdTinByLuong(queryCapBac, mucluong));
            }
            else
            {
                priceResult = priceResult.Concat(queryCapBac);
            }

            var result = priceResult.Distinct();

            var query = (from ct in DbContext.CongTys
                         join ttd in DbContext.TinTuyenDungs
                         on ct.Id equals ttd.CongTyId
                         join ttdung in result
                         on ttd.Id equals ttdung

                         select new
                         {
                             Id = ttd.Id,
                             ChucDanh = ttd.ChucDanh,
                             TuLuong = ttd.TuLuong,
                             DenLuong = ttd.DenLuong,
                             HienThiLuong = ttd.HienThiLuong,
                             TenCongTy = ct.Ten,
                             Logo = ct.Logo,
                             NgayDang = ttd.UpdatedDate,
                             Alias = ttd.Alias
                         }).AsEnumerable().Select(x => new TinTuyenDungVm()
                         {
                             Id = x.Id,
                             ChucDanh = x.ChucDanh,
                             TuLuong = x.TuLuong,
                             DenLuong = x.DenLuong,
                             HienThiLuong = x.HienThiLuong,
                             TenCongTy = x.TenCongTy,
                             Logo = x.Logo,
                             NgayDang = x.NgayDang,
                             Alias = x.Alias
                         });

            switch (sort)
            {
                case "salary":
                    query = query.OrderByDescending(x => x.DenLuong);
                    break;

                default:
                    query = query.OrderByDescending(x => x.NgayDang);
                    break;
            }

            return query;
        }

        public IEnumerable<int> GetIdTinByNganhNghe(IEnumerable<int> tinTuyenDung, int id)
        {
            var query = from ttdnn in DbContext.TinTuyenDungNganhNghes
                        join ttd in tinTuyenDung
                        on ttdnn.TinTuyenDungId equals ttd
                        where ttdnn.NganhNgheId.Equals(id)
                        select ttd;
            return query;
        }

        public IEnumerable<int> GetIdTinByTinh(IEnumerable<int> tinTuyenDung, int id)
        {
            var query = from ttdt in DbContext.TinTuyenDungTinhs
                        join ttd in tinTuyenDung
                        on ttdt.TinTuyenDungId equals ttd
                        where ttdt.TinhId.Equals(id)
                        select ttd;
            return query;
        }

        public IEnumerable<int> GetIdTinByKyNang(IEnumerable<int> tinTuyenDung, int id)
        {
            var query = from ttdkn in DbContext.TinTuyenDungKyNangs
                        join ttd in tinTuyenDung
                        on ttdkn.TinTuyenDungId equals ttd
                        where ttdkn.KyNangId.Equals(id)
                        select ttd;
            return query;
        }

        public IEnumerable<int> GetIdTinByCapBac(IEnumerable<int> tinTuyenDung, int id)
        {
            var query = from ttdung in DbContext.TinTuyenDungs
                        join ttd in tinTuyenDung
                        on ttdung.Id equals ttd
                        where ttdung.CapBacId.Equals(id)
                        select ttdung.Id;
            return query;
        }

        public IEnumerable<int> GetIdTinByLuong(IEnumerable<int> tinTuyenDung, string luong)
        {
            var query = from ttdung in DbContext.TinTuyenDungs
                        join ttd in tinTuyenDung
                        on ttdung.Id equals ttd
                        select ttdung;

            IEnumerable<int> result = Enumerable.Empty<int>();

            if (luong == "500")
            {
                result = result.Concat(query.Where(x => x.DenLuong <= 500).Select(y => y.Id));
            }
            else
            {
                if (luong == "500-1000")
                {
                    result = result.Concat(query.Where(x => x.TuLuong >= 500 && x.DenLuong <= 1000).Select(y => y.Id));
                }
                else
                {
                    if (luong == "1000-1500")
                    {
                        result = result.Concat(query.Where(x => x.TuLuong >= 1000 && x.DenLuong <= 1500).Select(y => y.Id));
                    }
                    else
                    {
                        if (luong == "1500-2000")
                        {
                            result = result.Concat(query.Where(x => x.TuLuong >= 1500 && x.DenLuong <= 2000).Select(y => y.Id));
                        }
                        else
                        {
                            if (luong == "2000-3000")
                            {
                                result = result.Concat(query.Where(x => x.TuLuong >= 2000 && x.DenLuong <= 3000).Select(y => y.Id));
                            }
                            else
                            {
                                if (luong == "3000")
                                    result = result.Concat(query.Where(x => x.TuLuong >= 3000).Select(y => y.Id));
                            }
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<TinhVm> GetTinh(IEnumerable<int> tinTuyenDung)
        {
            var query = (from t in DbContext.Tinhs
                         join ttdt in DbContext.TinTuyenDungTinhs
                         on t.Id equals ttdt.TinhId
                         join ttd in tinTuyenDung
                         on ttdt.TinTuyenDungId equals ttd
                         where t.Status
                         group t by new
                         {
                             t.Id,
                             t.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new TinhVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<NganhNgheVm> GetNganhNghe(IEnumerable<int> tinTuyenDung)
        {
            var query = (from nn in DbContext.NganhNghes
                         join ttdnn in DbContext.TinTuyenDungNganhNghes
                         on nn.Id equals ttdnn.NganhNgheId
                         join ttd in tinTuyenDung
                         on ttdnn.TinTuyenDungId equals ttd
                         where nn.Status
                         group nn by new
                         {
                             nn.Id,
                             nn.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new NganhNgheVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<CapBacVm> GetCapBac(IEnumerable<int> tinTuyenDung)
        {
            var query = (from cb in DbContext.CapBacs
                         join ttdung in DbContext.TinTuyenDungs
                         on cb.Id equals ttdung.CapBacId
                         join ttd in tinTuyenDung
                         on ttdung.Id equals ttd
                         where cb.Status
                         group cb by new
                         {
                             cb.Id,
                             cb.Ten
                         }
                         into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new CapBacVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         }

                         );
            return query;
        }

        public IEnumerable<KyNangVm> GetKyNangByTinId(int id)
        {
            var query = (from kn in DbContext.KyNangs
                         join ttdkn in DbContext.TinTuyenDungKyNangs
                         on kn.Id equals ttdkn.KyNangId
                         join ttd in DbContext.TinTuyenDungs
                         on ttdkn.TinTuyenDungId equals ttd.Id
                         where ttd.Id == id && kn.Status
                         group kn by new
                         {
                             kn.Id,
                             kn.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new KyNangVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<PhucLoiVm> GetPhucLoiByTinId(int id)
        {
            var query = (from pl in DbContext.PhucLois
                         join ctpl in DbContext.CongTyPhucLois
                         on pl.Id equals ctpl.PhucLoiId
                         join ct in DbContext.CongTys
                         on ctpl.CongTyId equals ct.Id
                         where ct.Id == id && pl.Status
                         group pl by new
                         {
                             pl.Id,
                             pl.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten
                         }).AsEnumerable().Select(x => new PhucLoiVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<NganhNgheVm> GetNganhNgheByTinId(int id)
        {
            var query = (from nn in DbContext.NganhNghes
                         join ttdnn in DbContext.TinTuyenDungNganhNghes
                         on nn.Id equals ttdnn.NganhNgheId
                         join ttd in DbContext.TinTuyenDungs
                         on ttdnn.TinTuyenDungId equals ttd.Id
                         where ttd.Id == id && nn.Status
                         group nn by new
                         {
                             nn.Id,
                             nn.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new NganhNgheVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }

        public IEnumerable<TinhVm> GetTinhByTinId(int id)
        {
            var query = (from t in DbContext.Tinhs
                         join ttdt in DbContext.TinTuyenDungTinhs
                         on t.Id equals ttdt.TinhId
                         join ttd in DbContext.TinTuyenDungs
                         on ttdt.TinTuyenDungId equals ttd.Id
                         where ttd.Id == id && t.Status
                         group t by new
                         {
                             t.Id,
                             t.Ten
                         } into g
                         select new
                         {
                             Id = g.Key.Id,
                             Ten = g.Key.Ten,
                         }).AsEnumerable().Select(x => new TinhVm()
                         {
                             Id = x.Id,
                             Ten = x.Ten
                         });
            return query;
        }
    }
}