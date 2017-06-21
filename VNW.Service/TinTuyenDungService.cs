using System.Collections.Generic;
using System.Linq;
using VNW.Data.Infrastructure;
using VNW.Data.Repositories;

namespace VNW.Service
{
    public interface ITinTuyenDungService
    {
        IEnumerable<string> GetListTinByName(string name);
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

        public IEnumerable<string> GetListTinByName(string name)
        {
            return _tinTuyenDungRepository.GetMulti(x => x.Status && x.ChucDang.Contains(name)).Select(y => y.ChucDang);
        }
    }
}