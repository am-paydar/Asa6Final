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
        //private UploadFile _uploadFile = new UploadFile();

        public PersonImageController(IPersonService personService)
        {
            _personService = personService;
        }


        //[HttpGet]
        //public async Task<ActionResult> GetImagePerson([FromForm] GetFile file)
        //{
        //    var person = await _personService.GetByReceivedIdNameAsync(file.ID, file.Name);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }
        //    switch (file.FileSize)
        //    {
        //        case "1":
        //            return Ok(person.TinyPath);
        //        case "2":
        //            return Ok(person.NormalPath);
        //        case "4":
        //            return Ok(person.BigPath);
        //        default: return BadRequest();
        //    }
        //}

        [HttpPost("CreatePerson")]
        public async Task<ActionResult> CreatePersonImage([FromForm] PostFile file)
        {
            var person = await _personService.GetByReceivedIdAsync(file.ID);
            //if (person != null)
            //{
            //    return BadRequest();
            //}
            //string? path = await _uploadFile.Saveasync(file.FormFile, "Person", "Normal", file.ID);

            
            var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Host/PersonsImage",file.FormFile.FileName), FileMode.Create);

            file.FormFile.CopyToAsync(fileStream);

            //if (path == null)
            //{
            //    return BadRequest();
            //}

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


        //[HttpPut]
        //public async Task<ActionResult> UpdatePersonImageasync([FromForm] PostFile file)
        //{
        //    var PersonUpdate = await _personService.GetByReceivedIdNameAsync(file.ID, file.Name);
        //    if (PersonUpdate == null || PersonUpdate.IsRemove)
        //    {
        //        return NotFound();
        //    }

        //    string? path = await _uploadFile.Saveasync(file.FormFile, "Person", "Normal", file.ID);
        //    if (path == null)
        //    {
        //        return BadRequest();
        //    }
        //    var personEntity = new PersonEntity()
        //    {
        //        ReceivedID = PersonUpdate.ReceivedID,
        //        Name = file.Name,
        //        Type = file.FormFile.ContentType,
        //        CreatedOn = DateTime.Now,
        //        Flag = true,
        //        NormalPath = path,
        //        BigPath = null,
        //        TinyPath = null,

        //    };
        //    await _personService.UpdateAsync(personEntity);
        //    return Ok();
        //}
    }
}
