using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ITinTuyenDungNganhNgheRepository : IRepository<TinTuyenDungNganhNghe>
    {
    }

    public class TinTuyenDungNganhNgheRepository : RepositoryBase<TinTuyenDungNganhNghe>, ITinTuyenDungNganhNgheRepository
    {
        public TinTuyenDungNganhNgheRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}