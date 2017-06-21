using System.Collections.Generic;
using System.Linq;
using VNW.Data.Infrastructure;
using VNW.Data.Repositories;
using VNW.Data.ViewModel;

namespace VNW.Service
{
    public interface ITinTuyenDungService
    {
        IEnumerable<string> GetListTinByName(string name);

        IEnumerable<TinTuyenDungVm> GetListBeginTin(string keyword, string industry, string location);
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

        public IEnumerable<TinTuyenDungVm> GetListBeginTin(string keyword, string industry, string location)
        {
            return _tinTuyenDungRepository.GetListBeginTin(keyword, industry, location);
        }

        public IEnumerable<string> GetListTinByName(string name)
        {
            return _tinTuyenDungRepository.GetMulti(x => x.Status && x.ChucDanh.Contains(name)).Select(y => y.ChucDanh);
        }
    }
}