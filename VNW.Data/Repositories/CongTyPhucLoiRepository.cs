using VNW.Data.Infrastructure;
using VNW.Data.Models;

namespace VNW.Data.Repositories
{
    public interface ICongTyPhucLoiRepository : IRepository<CongTyPhucLoi>
    {
    }

    public class CongTyPhucLoiRepository : RepositoryBase<CongTyPhucLoi>, ICongTyPhucLoiRepository
    {
        public CongTyPhucLoiRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}