using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface IPhucLoiRepository : IRepository<PhucLoi>
    {
    }

    public class PhucLoiRepository : RepositoryBase<PhucLoi>, IPhucLoiRepository
    {
        public PhucLoiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}