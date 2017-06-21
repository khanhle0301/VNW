using System.Collections.Generic;
using VNW.Data.Infrastructure;
using VNW.Data.Models;
using VNW.Data.Repositories;

namespace VNW.Service
{
    public interface INganhNgheService
    {
        NganhNghe Add(NganhNghe NganhNghe);

        void Update(NganhNghe NganhNghe);

        NganhNghe Delete(int id);

        IEnumerable<NganhNghe> GetAll();

        IEnumerable<NganhNghe> GetAll(string keyword);

        NganhNghe GetById(int id);

        void Save();
    }

    public class NganhNgheService : INganhNgheService
    {
        private INganhNgheRepository _NganhNgheRepository;
        private IUnitOfWork _unitOfWork;

        public NganhNgheService(INganhNgheRepository NganhNgheRepository, IUnitOfWork unitOfWork)
        {
            this._NganhNgheRepository = NganhNgheRepository;
            this._unitOfWork = unitOfWork;
        }

        public NganhNghe Add(NganhNghe NganhNghe)
        {
            return _NganhNgheRepository.Add(NganhNghe);
        }

        public NganhNghe Delete(int id)
        {
            return _NganhNgheRepository.Delete(id);
        }

        public IEnumerable<NganhNghe> GetAll()
        {
            return _NganhNgheRepository.GetAll();
        }

        public IEnumerable<NganhNghe> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _NganhNgheRepository.GetMulti(x => x.Ten.Contains(keyword));
            else
                return _NganhNgheRepository.GetAll();
        }

        public NganhNghe GetById(int id)
        {
            return _NganhNgheRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(NganhNghe NganhNghe)
        {
            _NganhNgheRepository.Update(NganhNghe);
        }
    }
}