using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ICongTyRepository : IRepository<CongTy>
    {
    }

    public class CongTyRepository : RepositoryBase<CongTy>, ICongTyRepository
    {
        public CongTyRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}