using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : DomainEntity
    {
        IEnumerable<TEntity> FindAll();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<IEnumerable<TEntity>> FindAllFalseFlag();
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetByReceivedIdAsync(string receivedCode);
        Task<TEntity?> GetByReceivedIdNameAsync(string receivedCode);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
