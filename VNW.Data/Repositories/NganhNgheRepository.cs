using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface INganhNgheRepository : IRepository<NganhNghe>
    {
    }

    public class NganhNgheRepository : RepositoryBase<NganhNghe>, INganhNgheRepository
    {
        public NganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}