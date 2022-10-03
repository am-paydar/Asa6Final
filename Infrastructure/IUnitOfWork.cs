using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
