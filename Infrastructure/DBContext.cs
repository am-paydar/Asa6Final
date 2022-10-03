using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MainContext : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("SECURED!");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageEntityConfig());
            modelBuilder.ApplyConfiguration(new MediaEntityConfig());
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public int SaveChanges()
        {
            return base.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}