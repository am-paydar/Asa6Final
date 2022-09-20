using Application.Services;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asa_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonImageController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonImageController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet("GetImagePerson")]
        public async Task<ActionResult> GetImagePersonAsync([FromForm] GetFileDTO file)
        {
            var person = await _personService.GetByReceivedIdAsync(file.ID);

            if (person == null)
            {
                return NotFound();
            }

            switch (file.FileSize)
            {
                case "Tiny":
                    return Ok(person.TinyPath);
                case "Normal":
                    return Ok(person.NormalPath);
                case "Big":
                    return Ok(person.BigPath);
                default: return BadRequest();
            }
        }


        [HttpPost("CreatePersonImage")]
        public async Task<ActionResult> CreatePersonImageAsync([FromForm] PostFileDTO file)
        {

            if(file.ID==null && file.FormFile==null)
                return BadRequest();

            var person = await _personService.GetByReceivedIdAsync(file.ID);

                if (person != null)
                {
                    return BadRequest();
                }
         


            //---------------------------------------
            var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Host/PersonsImage",file.FormFile.FileName), FileMode.Create);

            file.FormFile.CopyToAsync(fileStream);

            //---------------------------------------

            var personEntity = new PersonEntity()
            {
                ReceivedID = file.ID,
                Name = file.FormFile.Name,
                Type = file.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                Flag = true,
                NormalPath = Path.Combine(Directory.GetCurrentDirectory(), "Host", "PersonsImage", file.FormFile.FileName)
            };

            await _personService.CreateAsync(personEntity);

            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> UpdatePersonImageasync([FromForm] PutFileDTO file)
        {
            var PersonUpdate = await _personService.GetByReceivedIdAsync(file.RecivedID);

            if (PersonUpdate == null || PersonUpdate.IsRemove)
            {
                return NotFound();
            }

            string? path =  file.path;
            if (path == null)
            {
                return BadRequest();
            }
            var personEntity = new PersonEntity()
            {
                ReceivedID = PersonUpdate.ReceivedID,
                Name = file.FormFile.Name,
                Type = file.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                Flag = true,
                NormalPath = path,
                BigPath = null,
                TinyPath = null,

            };
            await _personService.UpdateAsync(personEntity);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> DeletePersonImageasync([FromForm] PutFileDTO file)
        {
            var PersonUpdate = await _personService.GetByReceivedIdAsync(file.RecivedID);

            if (PersonUpdate == null || PersonUpdate.IsRemove)
            {
                return NotFound();
            }

            string? path = file.path;

            if (path == null)
            {
                return BadRequest();
            }

            PersonUpdate.IsRemove = true;

            await _personService.UpdateAsync(PersonUpdate);
            return Ok();
        }
    }
}
