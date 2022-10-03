using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Application.CommonServices.UploadFile.Image
{
    public class UploadImageFile : IUploadImageFile
    {
        private IHostingEnvironment _hostingEnvironment;
        public UploadImageFile(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public bool ValidateFile(IFormFile file)
        {
            if (CheckFileType(file) && CheckFileSize(file))
            {
                return true;
            }
            return false;
        }
        private bool CheckFileType(IFormFile file)
        {
            if (!Allowedtypes().ContainsValue(file.ContentType))
            {
                return false;
            }
            return true;
        }
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
        private bool CheckFileSize(IFormFile file)
        {
            var size =(double) file.Length / 1024 / 1024;

            if( size == 0 )
            {
                return false;
            }
            if (size > 500 )
            {
                return false;
            }
            return true;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var filepath = CreateFilePath(Path.Combine(_hostingEnvironment.WebRootPath,"Images"),
                System.IO.Path.GetExtension(file.FileName));
            var filestream = File.Create(filepath);
            await file.CopyToAsync(filestream);
            filestream.Close();
            return GenerateServerFilePath(Path.GetFileName(filepath));
        }
        private string CreateFilePath(string FolderName,string fileExtention)
        {
            return Path.Combine(FolderName, FileName() + fileExtention);
        }
        private string FileName()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        private string GenerateServerFilePath(string fileName)
        {
            return "http://asa6.am-paydar.ir/File/Get?FolderName=" +
                   "Images" + "&fileName=" + fileName;
        }
    }
}
