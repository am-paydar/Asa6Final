using Domain.Models;

namespace Domain.IRepository
{
    public interface IImageRepository :IGenericRepository<ImageEntity>
    {
        Task<IEnumerable<ImageEntity>> FindAlltrueFlag();
    }
}
