using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.MediaServices
{
    public interface IMediaService : IGenericRepository<MediaEntity>
    {
        bool CheckFile(IFormFile file);
        Task<MediaEntity?> GetMedia(string id);
        Task<string> CreateMediaAsync(PostFileDTO file);
        void DeleteMedia(MediaEntity media);
    }
}
