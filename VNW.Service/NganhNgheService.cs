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

        IEnumerable<NganhNghe> GetAllByChild();
    }

    public class NganhNgheService : INganhNgheService
    {
        private INganhNgheRepository _nganhNgheRepository;
        private IUnitOfWork _unitOfWork;

        public NganhNgheService(INganhNgheRepository nganhNgheRepository, IUnitOfWork unitOfWork)
        {
            this._nganhNgheRepository = nganhNgheRepository;
            this._unitOfWork = unitOfWork;
        }

        public NganhNghe Add(NganhNghe NganhNghe)
        {
            return _nganhNgheRepository.Add(NganhNghe);
        }

        public NganhNghe Delete(int id)
        {
            return _nganhNgheRepository.Delete(id);
        }

        public IEnumerable<NganhNghe> GetAll()
        {
            return _nganhNgheRepository.GetAll();
        }

        public IEnumerable<NganhNghe> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _nganhNgheRepository.GetMulti(x => x.Ten.Contains(keyword));
            else
                return _nganhNgheRepository.GetAll();
        }

        public IEnumerable<NganhNghe> GetAllByChild()
        {
            return _nganhNgheRepository.GetMulti(x => x.Status && x.ParentId != null);
        }

        public NganhNghe GetById(int id)
        {
            return _nganhNgheRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(NganhNghe NganhNghe)
        {
            _nganhNgheRepository.Update(NganhNghe);
        }
    }
}