using Microsoft.AspNetCore.Http;

namespace Domain.DTO
{
    public class PutFileDTO
    {
        public string ID { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
