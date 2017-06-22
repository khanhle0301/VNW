using System;
using System.Collections.Generic;
using System.Linq;
using VNW.Data.Infrastructure;
using VNW.Data.Models;
using VNW.Data.Repositories;
using VNW.Data.ViewModel;

namespace VNW.Service
{
    public interface ITinTuyenDungService
    {
        IEnumerable<string> GetListTinByName(string name);

        IEnumerable<KyNangVm> GetKyNang(IEnumerable<int> tinTuyenDung);

        IEnumerable<TinhVm> GetTinh(IEnumerable<int> tinTuyenDung);

        IEnumerable<NganhNgheVm> GetNganhNghe(IEnumerable<int> tinTuyenDung);

        IEnumerable<CapBacVm> GetCapBac(IEnumerable<int> tinTuyenDung);

        IEnumerable<int> GetListBeginTin(string keyword, string industry, string location);

        IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location, string sort,
            string nganhnghe, string diadiem, string kynang, string capbac, string mucluong);

        TinTuyenDung GetById(int id);

        IEnumerable<KyNangVm> GetKyNangByTinId(int id);

        IEnumerable<PhucLoiVm> GetPhucLoiByTinId(int id);

        IEnumerable<NganhNgheVm> GetNganhNgheByTinId(int id);

        IEnumerable<TinhVm> GetTinhByTinId(int id);

        void IncreaseView(int id);

        void Save();
    }

    public class TinTuyenDungService : ITinTuyenDungService
    {
        private ITinTuyenDungRepository _tinTuyenDungRepository;
        private IUnitOfWork _unitOfWork;

        public TinTuyenDungService(ITinTuyenDungRepository tinTuyenDungRepository, IUnitOfWork unitOfWork)
        {
            this._tinTuyenDungRepository = tinTuyenDungRepository;
            this._unitOfWork = unitOfWork;
        }

        public TinTuyenDung GetById(int id)
        {
            return _tinTuyenDungRepository.GetSingleByCondition(x => x.Id == id, new string[] { "CongTy", "CapBac" });
        }

        public IEnumerable<CapBacVm> GetCapBac(IEnumerable<int> tinTuyenDung)
        {
            return _tinTuyenDungRepository.GetCapBac(tinTuyenDung);
        }

        public IEnumerable<KyNangVm> GetKyNang(IEnumerable<int> tinTuyenDung)
        {
            return _tinTuyenDungRepository.GetKyNang(tinTuyenDung);
        }

        public IEnumerable<KyNangVm> GetKyNangByTinId(int id)
        {
            return _tinTuyenDungRepository.GetKyNangByTinId(id);
        }

        public IEnumerable<int> GetListBeginTin(string keyword, string industry, string location)
        {
            return _tinTuyenDungRepository.GetListBeginTin(keyword, industry, location);
        }

        public IEnumerable<TinTuyenDungVm> GetListSearch(string keyword, string industry, string location, string sort, string nganhnghe, string diadiem, string kynang, string capbac, string mucluong)
        {
            return _tinTuyenDungRepository.GetListSearch(keyword, industry, location, sort,
             nganhnghe, diadiem, kynang, capbac, mucluong);
        }

        public IEnumerable<string> GetListTinByName(string name)
        {
            return _tinTuyenDungRepository.GetMulti(x => x.Status && x.ChucDanh.Contains(name)).Select(y => y.ChucDanh);
        }

        public IEnumerable<NganhNgheVm> GetNganhNghe(IEnumerable<int> tinTuyenDung)
        {
            return _tinTuyenDungRepository.GetNganhNghe(tinTuyenDung);
        }

        public IEnumerable<NganhNgheVm> GetNganhNgheByTinId(int id)
        {
            return _tinTuyenDungRepository.GetNganhNgheByTinId(id);
        }

        public IEnumerable<PhucLoiVm> GetPhucLoiByTinId(int id)
        {
            return _tinTuyenDungRepository.GetPhucLoiByTinId(id);
        }

        public IEnumerable<TinhVm> GetTinh(IEnumerable<int> tinTuyenDung)
        {
            return _tinTuyenDungRepository.GetTinh(tinTuyenDung);
        }

        public IEnumerable<TinhVm> GetTinhByTinId(int id)
        {
            return _tinTuyenDungRepository.GetTinhByTinId(id);
        }

        public void IncreaseView(int id)
        {
            var tin = _tinTuyenDungRepository.GetSingleById(id);
            if (tin.ViewCount.HasValue)
                tin.ViewCount += 1;
            else
                tin.ViewCount = 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}