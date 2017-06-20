using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ITinTuyenDungKyNangRepository : IRepository<TinTuyenDungKyNang>
    {
    }

    public class TinTuyenDungKyNangRepository : RepositoryBase<TinTuyenDungKyNang>, ITinTuyenDungKyNangRepository
    {
        public TinTuyenDungKyNangRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}