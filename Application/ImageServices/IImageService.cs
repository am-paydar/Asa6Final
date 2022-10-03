using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.ImageServices
{
    public interface IImageService : IImageRepository
    {
        bool CheckFile(IFormFile file);
        Task<string> CreateImageAsync(PostFileDTO file);
        Task<ImageEntity?> GetImage(string id);
        string GetTinyImage(ImageEntity image);
        string GetBigImage(ImageEntity image);
        Task UpdateImage(PutFileDTO file,ImageEntity updateImage);
        void DeleteImage(ImageEntity deleteImage);
    }
}
