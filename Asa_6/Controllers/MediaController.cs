using Application.MediaServices;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asa_6.Controllers
{
    [Route("api/Media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _MediaService;
        public MediaController(IMediaService mediaService)
        {
            _MediaService = mediaService;
        }

        [HttpGet("GetMedia")]
        public async Task<ActionResult> GetMediaAsync(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }
            var mediaEntity = await _MediaService.GetMedia(ID);

            if (mediaEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            return Ok(mediaEntity.NormalPath);
        }
        
        [HttpPost("CreateMedia")]
        public async Task<ActionResult<string>> CreateMediaAsync([FromForm] PostFileDTO file)
        {
            if (!_MediaService.CheckFile(file.FormFile))
            {
                return BadRequest("The file doesn't valid ");
            }
            return Ok(await _MediaService.CreateMediaAsync(file));
        }
         
        [HttpDelete("DeleteMedia")]
        public async Task<ActionResult> DeleteMediaAsync(string ID)
        {
            if (ID == null)
            {
                return BadRequest("ID is null");
            }
            var mediaEntity = await _MediaService.GetMedia(ID);

            if (mediaEntity == null)
            {
                return NotFound("The ID doesn't exist");
            }
            _MediaService.DeleteMedia(mediaEntity);
            return Ok();
        }
    }
}
