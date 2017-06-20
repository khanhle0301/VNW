namespace VNW.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private VNWDbContext dbContext;

        public VNWDbContext Init()
        {
            return dbContext ?? (dbContext = new VNWDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}