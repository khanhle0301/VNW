using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ITinhRepository : IRepository<Tinh>
    {
    }

    public class TinhRepository : RepositoryBase<Tinh>, ITinhRepository
    {
        public TinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}