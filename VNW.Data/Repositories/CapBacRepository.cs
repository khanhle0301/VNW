using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ICapBacRepository : IRepository<CapBac>
    {
    }

    public class CapBacRepository : RepositoryBase<CapBac>, ICapBacRepository
    {
        public CapBacRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}