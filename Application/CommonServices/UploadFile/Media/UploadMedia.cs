using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.CommonServices.UploadFile.Media
{
    public class UploadMedia : IUploadMedia
    {
        private IHostingEnvironment _hostingEnvironment;
        public UploadMedia(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var filepath = CreateFilePath(Path.Combine(_hostingEnvironment.WebRootPath, "Media"),
                System.IO.Path.GetExtension(file.FileName));
            var filestream = File.Create(filepath);
            await file.CopyToAsync(filestream);
            filestream.Close();
            return GenerateServerFilePath(Path.GetFileName(filepath));
        }
        private string CreateFilePath(string FolderName, string fileExtention)
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
                   "Media" + "&fileName=" + fileName;
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
                {".MP3","audio/mpeg"},
                {".OGG","audio/ogg"},
                {".WAV","audio/wav"},
                {".MP4","video/mp4"},
                {".Ogg","video/ogg"},
                {".WebM","video/webm"}
            };
        }
        private bool CheckFileSize(IFormFile file)
        {
            var size = (double)file.Length / 1024 / 1024;

            if (size == 0)
            {
                return false;
            }
            if (size > 50)
            {
                return false;
            }
            return true;
        }
    }
}
