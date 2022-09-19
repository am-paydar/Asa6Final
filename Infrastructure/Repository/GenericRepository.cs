using Domain.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<TEntity> FindAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _unitOfWork.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _unitOfWork.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _unitOfWork.SaveChanges();

        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllFalseFlag()
        {
            return await FindByCondition(entity => entity.Flag.Equals(false)).ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await FindByCondition(entity => entity.ID.Equals(id))
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetByReceivedIdAsync(string receivedCode)
        {
            return await FindByCondition(entity => entity.ReceivedID.Equals(receivedCode))
                .ToListAsync();
        }
        public async Task<TEntity?> GetByReceivedIdNameAsync(string receivedCode, string name)
        {
            return await FindByCondition(entity => entity.ReceivedID.Equals(receivedCode) && entity.Name.Equals(name))
                .FirstOrDefaultAsync();
        }
        public async Task CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
