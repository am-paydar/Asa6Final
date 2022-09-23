using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    
        public interface IPersonService : IGenericRepository<PersonEntity>
        {    
        Task<PersonEntity> GetPersonAsync(string receivedCode);
        Task<Tuple<bool, PersonEntity>> CheckPersonIdAsync(string receivedCode);
        Task CreatePersonImageAsync(PostFileDTO file, string path);

        Task<string>GetPersonNormalImageAsync(string receivedCode);

        Task<string> GetPersonTinyImageAsync(string receivedCode);

        Task<string> GetPersonBigImageAsync(string receivedCode);

        Task UpdatePersonImageAsync(PutFileDTO file, string path);

        Task DeletePersonImageasync(string RecivedID);

        

    }

    
}
