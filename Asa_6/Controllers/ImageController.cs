using Application.ImageServices;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Asa_6.Controllers
{
    [Route("api/Image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _ImageService;
        public ImageController(IImageService imageService)
        {
            _ImageService = imageService;
        }

        [HttpGet("GetNormalImage")]
        public async Task<ActionResult> GetNormalImage(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }
            var imageEntity = await _ImageService.GetImage(ID);

            if (imageEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            return Ok(imageEntity.NormalPath);
        }

        [HttpGet("GetTinyImage")]
        public async Task<ActionResult> GetTinyImage(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }

            var imageEntity = await _ImageService.GetImage(ID);

            if (imageEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            return Ok(_ImageService.GetTinyImage(imageEntity));
        }

        [HttpGet("GetBigImage")]
        public async Task<ActionResult> GetBigImage(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }

            var imageEntity = await _ImageService.GetImage(ID);

            if (imageEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            return Ok(_ImageService.GetBigImage(imageEntity));
        }

        [HttpPost("CreateImage")]
        public async Task<ActionResult> CreateImageAsync([FromForm] PostFileDTO file)
        {
            if (!_ImageService.CheckFile(file.FormFile))
            {
                return BadRequest("The file doesn't valid ");
            }
            return Ok(await _ImageService.CreateImageAsync(file));
        }

        [HttpPut("UpdateImage")]
        public async Task<ActionResult> UpdateImageAsync([FromForm] PutFileDTO file)
        {
            if (file.ID == null)
            {
                return BadRequest("ID is null");
            }
            if (!_ImageService.CheckFile(file.FormFile))
            {
                return BadRequest("The file doesn't valid ");
            }

            var imageEntity = await _ImageService.GetImage(file.ID);

            if (imageEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }

            await _ImageService.UpdateImage(file, imageEntity);
            return Ok();
        }

        [HttpDelete("DeleteImage")]
        public async Task<ActionResult> DeleteImageAsync(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }
            var imageEntity = await _ImageService.GetImage(ID);

            if (imageEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            _ImageService.DeleteImage(imageEntity);
            return Ok();
        }

    }
}
