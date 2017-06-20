using System;

namespace VNW.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        VNWDbContext Init();
    }
}