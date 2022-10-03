using Application.CommonServices.Hash;
using Application.CommonServices.UploadFile.Media;
using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.MediaServices
{
    public class MediaService : GenericRepository<MediaEntity>, IMediaService
    {
        private readonly IGenericRepository<MediaEntity> _MediaRepository;
        private readonly IUploadMedia _UploadMedia;
        private readonly IHash _Hash;
        public MediaService(IUnitOfWork unitOfWork, IGenericRepository<MediaEntity> mediaRepository, IHash hash, IUploadMedia UploadMedia) : base(
            unitOfWork)
        {
            _MediaRepository = mediaRepository;
            _Hash = hash;
            _UploadMedia = UploadMedia;
        }
        public bool CheckFile(IFormFile file)
        {
            return _UploadMedia.ValidateFile(file);
        }
        public async Task<MediaEntity?> GetMedia(string id)
        {
            return await _MediaRepository.GetByIdAsync(int.Parse(DecodeMediaID(id)));
        }
        private string DecodeMediaID(string id)
        {
             return _Hash.DecodingTxT(id);
        }
        public async Task<string> CreateMediaAsync(PostFileDTO file)
        {
            var NewMedia = new MediaEntity()
            {
                Name = file.FormFile.FileName,
                Type = file.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                NormalPath = await _UploadMedia.SaveFileAsync(file.FormFile),
            };
            return EncodeMediaID(await _MediaRepository.CreateAsync(NewMedia));
        }
        private string EncodeMediaID(int id)
        {
            return _Hash.EncodingTxT(id.ToString());
        }
        public void DeleteMedia(MediaEntity media)
        {
            media.IsRemove = true;
            _MediaRepository.Update(media);
        }
    }
}
