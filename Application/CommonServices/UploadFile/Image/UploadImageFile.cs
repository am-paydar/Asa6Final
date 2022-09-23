using Application.CommonServices.UploadFile.Image;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommonServices.UploadFile.Image
{
    public class UploadImageFile : IUploadImageFile
    {

        private bool CheckFileType(IFormFile file)
        {
            if (!Allowedtypes().ContainsValue(file.ContentType))
            {
                return false;
            }
            return true;
        }
        private bool CheckFileSize(IFormFile file)
        {
            var size = file.Length / 1024 / 1024;
            if (size > 500)
            {
                return false;
            }

            return true;
        }
        

        private string CrateFilePath(string FolderName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(),"Host", FolderName, FileName());
        }

        //public string CrateFilePath()
        //{
        //    return Path.Combine( , FileName());
        //}

        private Dictionary<string, string> Allowedtypes()
        {
            return new Dictionary<string, string>
            {
                {".jpg","image/jpeg"},
                {".png","image/png"},
                {".jpeg","image/jpeg"},
                {".gif","image/gif"}
            };
        }

        private string FileName()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public bool ValidateFile(IFormFile file)
        {
            if(CheckFileType(file) && CheckFileSize(file))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         public async  Task<string> SaveFileAsync(IFormFile file, string FolderName)
        {
            var filepath = CrateFilePath(FolderName);
            var filestream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(filestream);
            return filepath;
        }
    }
}
