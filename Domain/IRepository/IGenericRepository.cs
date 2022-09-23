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
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> FindAlltrueFlag();
        Task<IEnumerable<TEntity>> FindAllIsRemove();
        Task<TEntity?> GetByIdAsync(string receivedCode);
        Task<IEnumerable<TEntity>> GetByReceivedIdAsync(string receivedCode);
        Task<TEntity?> GetByNormalPathasync(string path);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

    }
}
