using Microsoft.AspNetCore.Http;

namespace Application.CommonServices.UploadFile.Image
{
    public  interface IUploadImageFile
    {
        bool ValidateFile(IFormFile file);
        Task<string> SaveFileAsync(IFormFile file);
    }
}
