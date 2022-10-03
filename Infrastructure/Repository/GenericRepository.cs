using Domain.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : DomainEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = unitOfWork.Set<TEntity>();
        }
        public async Task<int> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.ID;
        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _unitOfWork.SaveChanges();
        }
        //--------------------------------------------------------------
        protected IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(entity => entity.ID.Equals(id) && entity.IsRemove.Equals(false)).FirstOrDefaultAsync();
        }
    }
}
