using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ITinTuyenDungRepository : IRepository<TinTuyenDung>
    {
    }

    public class TinTuyenDungRepository : RepositoryBase<TinTuyenDung>, ITinTuyenDungRepository
    {
        public TinTuyenDungRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}