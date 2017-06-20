namespace VNW.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}