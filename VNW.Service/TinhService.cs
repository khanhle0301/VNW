using System.Collections.Generic;
using VNW.Data.Infrastructure;
using VNW.Data.Models;
using VNW.Data.Repositories;

namespace VNW.Service
{
    public interface ITinhService
    {
        IEnumerable<Tinh> GetAll();
    }

    public class TinhService : ITinhService
    {
        private ITinhRepository _tinhRepository;
        private IUnitOfWork _unitOfWork;

        public TinhService(ITinhRepository tinhRepository, IUnitOfWork unitOfWork)
        {
            this._tinhRepository = tinhRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Tinh> GetAll()
        {
            return _tinhRepository.GetAll();
        }
    }
}