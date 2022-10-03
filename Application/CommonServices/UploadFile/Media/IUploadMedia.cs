using Microsoft.AspNetCore.Http;

namespace Application.CommonServices.UploadFile.Media
{
    public interface IUploadMedia
    {
        bool ValidateFile(IFormFile file);
        Task<string> SaveFileAsync(IFormFile file);
    }
}
