using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PostFile
    {
        public string ID { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
