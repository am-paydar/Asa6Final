using Domain.Models;
using System.Linq.Expressions;

namespace Domain.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : DomainEntity
    {
        Task<int> CreateAsync(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetByIdAsync(int id);
    }
}
