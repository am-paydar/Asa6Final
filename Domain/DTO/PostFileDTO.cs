using Microsoft.AspNetCore.Http;
namespace Domain.DTO
{
    public class PostFileDTO
    {
        public IFormFile FormFile { get; set; }
    }
}
