using Domain.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ImageRepository : GenericRepository<ImageEntity>, IImageRepository
    {
        public ImageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<IEnumerable<ImageEntity>> FindAlltrueFlag()
        {
            return await FindByCondition(entity => entity.Flag.Equals(true)&&entity.IsRemove.Equals(false)).ToListAsync();
        }
    }
}
