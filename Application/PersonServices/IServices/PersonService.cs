using Application.Services;
using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public class PersonService : GenericRepository<PersonEntity>, IPersonService
    {
        public readonly IGenericRepository<PersonEntity> GenericRepository;

        public PersonService(IUnitOfWork unitOfWork, IGenericRepository<PersonEntity> genericRepository) : base(
            unitOfWork)
        {
            GenericRepository = genericRepository;
        }

        public async Task<PersonEntity> GetPersonAsync(string receivedCode)
        {
            return await GenericRepository.GetByIdAsync(receivedCode);
        }

        public async Task<Tuple<bool,PersonEntity>> CheckPersonIdAsync(string receivedCode)
        {
            var person = await GetPersonAsync(receivedCode);
            if (person!=null)
            {
                return Tuple.Create(false, person);
            }
            return Tuple.Create(true, person);
        }

        public async Task CreatePersonImageAsync(PostFileDTO file, string path)
        {
            var NewPerson = new PersonEntity()
            {
                ReceivedID = file.RecivedID,
                Name = file.FormFile.FileName,
                Type = file.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                Flag = true,
                NormalPath = path
            };
       
            await GenericRepository.CreateAsync(NewPerson);
         }

        public async Task<string> GetPersonNormalImageAsync(string receivedCode)
        {
           var person= await GetPersonAsync(receivedCode);

            if (person == null)
            {
                return null;
            }

            return person.NormalPath;
        }

        public async Task<string> GetPersonTinyImageAsync(string receivedCode)
        {
            var person = await GetPersonAsync(receivedCode);
            if (person == null)
            {
                return null;
            }
            if(person.TinyPath != null)
            {
                return person.TinyPath;
            }
            else
            {
                return person.NormalPath;

            }

        }

        public async Task<string> GetPersonBigImageAsync(string receivedCode)
        {
            var person = await GetPersonAsync(receivedCode);
            if (person == null)
            {
                return null;
            }
            if (person.BigPath != null)
            {
                return person.BigPath;
            }
            else
            {
                return person.NormalPath;

            }
        }

        public async Task UpdatePersonImageAsync(PutFileDTO file, string path)
        {
            var person = await GetPersonAsync(file.RecievedID);




            person.Name = file.FormFile.FileName;
            person.Type = file.FormFile.ContentType;
            person.CreatedOn = DateTime.Now;
            person.Flag = true;
            person.NormalPath = path;
            person.TinyPath = null;
            person.BigPath = null;

           
            await GenericRepository.UpdateAsync(person);
        }

        public async Task DeletePersonImageasync(string RecivedID)
        {
            var person = await GetPersonAsync(RecivedID);

            person.IsRemove = true;
           
            await GenericRepository.UpdateAsync(person);
        }


    }
}
