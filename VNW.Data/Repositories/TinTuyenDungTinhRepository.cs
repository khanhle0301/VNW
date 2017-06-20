using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ITinTuyenDungTinhRepository : IRepository<TinTuyenDungTinh>
    {
    }

    internal class TinTuyenDungTinhRepository : RepositoryBase<TinTuyenDungTinh>, ITinTuyenDungTinhRepository
    {
        public TinTuyenDungTinhRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}