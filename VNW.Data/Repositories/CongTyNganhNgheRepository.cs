using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ICongTyNganhNgheRepository : IRepository<CongTyNganhNghe>
    {
    }

    public class CongTyNganhNgheRepository : RepositoryBase<CongTyNganhNghe>, ICongTyNganhNgheRepository
    {
        public CongTyNganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}