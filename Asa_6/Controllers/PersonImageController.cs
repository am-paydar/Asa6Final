using Application.CommonServices.UploadFile.Image;
using Application.Services;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asa_6.Controllers
{
    [Route("api/PersonImage")]
    [ApiController]
    public class PersonImageController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IUploadImageFile _IUploadImageFile;

        public PersonImageController(IPersonService personService, IUploadImageFile iUploadImageFile)
        {
            _personService = personService;

            _IUploadImageFile = iUploadImageFile;
        }


        [HttpGet("GetPersonNormalImage/{id}")]
        public async Task<ActionResult> GetPersonNormalImageAsync(string RecivedCode)
        {
            var path = await _personService.GetPersonNormalImageAsync(RecivedCode);

            if (path==null)
            {
                return BadRequest("The person with this ID doesn't exists");
            }

            return Ok(path);

        }

        [HttpGet("GetPersonTinyImage/{id}")]
        public async Task<ActionResult> GetPersonTinyImageAsync(string RecivedCode)
        {
            var path = await _personService.GetPersonTinyImageAsync(RecivedCode);

            if (path==null)
            {
                return BadRequest("The person with this ID doesn't exists");
            }

            return Ok(path);

        }

        [HttpGet("GetPersonBigImage/{id}")]
        public async Task<ActionResult> GetPersonBigImageAsync(string RecivedCode)
        {
            var path = await _personService.GetPersonBigImageAsync(RecivedCode);

            if (path==null)
            {
                return BadRequest("The person with this ID doesn't exists");
            }
 
            return Ok(path);

        }


        [HttpPost("CreatePersonImage")]
        public async Task<ActionResult> CreatePersonImageAsync([FromForm] PostFileDTO file)
        {
            var tuple =await _personService.CheckPersonIdAsync(file.RecivedID);

            if (!tuple.Item1)
            {
                return BadRequest("The person with this ID has already exists");
            }

            if (!_IUploadImageFile.ValidateFile(file.FormFile))
            {
                return BadRequest("The file doesn't valid ");

            }

            var path=await _IUploadImageFile.SaveFileAsync(file.FormFile, "PersonsImages");

            _personService.CreatePersonImageAsync(file, path);

            return Ok();
        }


        [HttpPut("UpdatePersonImage")]
        public async Task<ActionResult> UpdatePersonImageasync([FromForm] PutFileDTO file)
        {
            var tuple = await _personService.CheckPersonIdAsync(file.RecievedID);

            if (tuple.Item1)
            {
                return BadRequest("The person with this ID doesn't exist");
            }

            if (!_IUploadImageFile.ValidateFile(file.FormFile))
            {
                return BadRequest("The file doesn't valid ");

            }

            var path = await _IUploadImageFile.SaveFileAsync(file.FormFile, "PersonsImages");

            await _personService.UpdatePersonImageAsync(file, path);

            return Ok();
        }


        
        [HttpDelete("DeletePersonImage")]
        public async Task<ActionResult> DeletePersonImageasync([FromForm] DeleteFileDTO file)
        {
            var tuple = await _personService.CheckPersonIdAsync(file.RecivedID);

            if (tuple.Item1)
            {
                return BadRequest("The person with this ID doesn't exist");
            }

            await _personService.DeletePersonImageasync(file.RecivedID);
            return Ok();
        }

    }
}
