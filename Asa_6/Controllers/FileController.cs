using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Asa_6.Controllers
{
    [Route("File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private FileExtensionContentTypeProvider _extension;
        private IHostingEnvironment _hostingEnvironment;
        public FileController(FileExtensionContentTypeProvider _extension, IHostingEnvironment environment)
        {
            this._extension = _extension;
            _hostingEnvironment = environment;
        }

        [HttpGet("Get")]
        public ActionResult Download(string FolderName, string fileName)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, FolderName + "/" + fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            if (!_extension.TryGetContentType(filePath,
                        out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(fileBytes, contentType, fileName);
        }

    }
}
