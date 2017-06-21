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

        IEnumerable<TinTuyenDungVm> GetListBeginTin(string keyword, string industry, string location);

        IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location, string sort, string nganhnghe, string diadiem, string kynang, string capbac, string mucluong);
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

        public IEnumerable<TinTuyenDungVm> GetListBeginTin(string keyword, string industry, string location)
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

            var query = (from ct in DbContext.CongTys
                         join ttd in DbContext.TinTuyenDungs
                         on ct.Id equals ttd.CongTyId
                         join ttdTinh in queryTinh
                         on ttd.Id equals ttdTinh

                         select new
                         {
                             Id = ttd.Id,
                             ChucDanh = ttd.ChucDanh,
                             TuLuong = ttd.TuLuong,
                             DenLuong = ttd.DenLuong,
                             HienThiLuong = ttd.HienThiLuong,
                             TenCongTy = ct.Ten,
                             Logo = ct.Logo,
                             NgayDang = ttd.UpdatedDate
                         }).AsEnumerable().Select(x => new TinTuyenDungVm()
                         {
                             Id = x.Id,
                             ChucDanh = x.ChucDanh,
                             TuLuong = x.TuLuong,
                             DenLuong = x.DenLuong,
                             HienThiLuong = x.HienThiLuong,
                             TenCongTy = x.TenCongTy,
                             Logo = x.Logo,
                             NgayDang = x.NgayDang
                         });

            return query;
        }

        public IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location,
            string sort, string nganhnghe, string diadiem, string kynang, string capbac, string mucluong)
        {
            var queryTTD = GetListBeginTin(keyword, industry, location).Select(x => x.Id);

            IEnumerable<int> queryNganhNghe = queryTTD;

            if (!string.IsNullOrEmpty(nganhnghe))
            {
                var typeNganhNghe = nganhnghe.Split(',');
                foreach (var item in typeNganhNghe)
                {
                    queryNganhNghe = queryNganhNghe.Concat(this.GetIdTinByNganhNghe(queryNganhNghe, int.Parse(item)));
                }
            }

            IEnumerable<int> queryDiaDiem = queryNganhNghe;

            if (!string.IsNullOrEmpty(diadiem))
            {
                var typeDiaDiem = diadiem.Split(',');
                foreach (var item in typeDiaDiem)
                {
                    queryDiaDiem = queryDiaDiem.Concat(this.GetIdTinByTinh(queryDiaDiem, int.Parse(item)));
                }
            }

            IEnumerable<int> queryKyNang = queryDiaDiem;

            if (!string.IsNullOrEmpty(kynang))
            {
                var typeKyNang = kynang.Split(',');
                foreach (var item in typeKyNang)
                {
                    queryKyNang = queryKyNang.Concat(this.GetIdTinByKyNang(queryKyNang, int.Parse(item)));
                }
            }

            IEnumerable<int> queryCapBac = queryKyNang;

            if (!string.IsNullOrEmpty(capbac))
            {
                var typeCapbac = capbac.Split(',');
                foreach (var item in typeCapbac)
                {
                    queryCapBac = queryCapBac.Concat(this.GetIdTinByCapBac(queryCapBac, int.Parse(item)));
                }
            }

            IEnumerable<int> priceResult = queryCapBac;

            if (!string.IsNullOrEmpty(mucluong))
            {
                var priceArr = mucluong.Split(',');
                foreach (var item in priceArr)
                {
                    priceResult = priceResult.Concat(this.GetIdTinByLuong(queryCapBac, item));
                }
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
                             NgayDang = ttd.UpdatedDate
                         }).AsEnumerable().Select(x => new TinTuyenDungVm()
                         {
                             Id = x.Id,
                             ChucDanh = x.ChucDanh,
                             TuLuong = x.TuLuong,
                             DenLuong = x.DenLuong,
                             HienThiLuong = x.HienThiLuong,
                             TenCongTy = x.TenCongTy,
                             Logo = x.Logo,
                             NgayDang = x.NgayDang
                         });

            switch (sort)
            {
                case "date":
                    query = query.OrderByDescending(x => x.NgayDang);
                    break;

                case "relevance":
                    query = query.OrderByDescending(x => x.TuLuong);
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

            IEnumerable<int> result = null;

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
                                result = result.Concat(query.Where(x => x.TuLuong >= 3000).Select(y => y.Id));
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}