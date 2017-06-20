using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface IKyNangRepository : IRepository<KyNang>
    {
    }

    public class KyNangRepository : RepositoryBase<KyNang>, IKyNangRepository
    {
        public KyNangRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}