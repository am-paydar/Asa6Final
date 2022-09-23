using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommonServices.UploadFile.Image
{
    public  interface IUploadImageFile
    {

        bool ValidateFile(IFormFile file);

        Task<string> SaveFileAsync(IFormFile file, string FolderName);

    }
}
