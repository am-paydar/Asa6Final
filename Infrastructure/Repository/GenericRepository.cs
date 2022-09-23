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
       
        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> FindAlltrueFlag()
        {
            return await FindByCondition(entity => entity.Flag.Equals(true)).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllIsRemove()
        {
            return await FindByCondition(entity => entity.Flag.Equals(true)).ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> GetByReceivedIdAsync(string receivedCode)
        {
            return await FindByCondition(entity => entity.ReceivedID.Equals(receivedCode))
                    .AsNoTracking().ToListAsync();

        }
        public async Task<TEntity?> GetByNormalPathasync(string path)
        {
            return await FindByCondition(entity => entity.NormalPath.Equals(path))
                .FirstOrDefaultAsync();
        }

        //--------------------------------------------------------------
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
        //--------------------------------------------------------------
        public async Task<TEntity?> GetByIdAsync(string receivedCode)
        {
            return await _dbSet.Where(entity=>entity.ReceivedID.Equals(receivedCode)&&entity.IsRemove.Equals(false)).FirstOrDefaultAsync();
        }
    }
}
