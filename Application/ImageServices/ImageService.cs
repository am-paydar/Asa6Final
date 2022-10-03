using Application.CommonServices.Hash;
using Application.CommonServices.UploadFile.Image;
using Domain.DTO;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.ImageServices
{
    public class ImageService : ImageRepository, IImageService
    {
        private readonly IImageRepository _ImageRepository;
        private readonly IUploadImageFile _UploadImageFile;
        private readonly IHash _Hash;
        public ImageService(IUnitOfWork unitOfWork, IHash hash, IImageRepository imageRepository, IUploadImageFile UploadImageFile) : base(unitOfWork)
        {
            _Hash = hash;
            _ImageRepository = imageRepository;
            _UploadImageFile = UploadImageFile;
        }
        public bool CheckFile(IFormFile file)
        {
            return _UploadImageFile.ValidateFile(file);
        }
        public async Task<ImageEntity?> GetImage(string id)
        {
            return await _ImageRepository.GetByIdAsync(int.Parse(DecodeImageID(id)));
        }
        private string DecodeImageID(string id)
        {
            return _Hash.DecodingTxT(id);
        }
        public string GetBigImage(ImageEntity image)
        {
            if (image.BigPath != null)
            {
                return image.BigPath;
            }
            return image.NormalPath;
        }
        public string GetTinyImage(ImageEntity image)
        {
            if (image.TinyPath != null)
            {
                return image.TinyPath;
            }
            return image.NormalPath;
        }
        public async Task<string> CreateImageAsync(PostFileDTO file)
        {
            var newImage = new ImageEntity
            {
                Name = file.FormFile.FileName,
                Type = file.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                Flag = true,
                NormalPath = await _UploadImageFile.SaveFileAsync(file.FormFile),
                TinyPath = null,
                BigPath = null
            };
            return EncodeImageID(await _ImageRepository.CreateAsync(newImage));
        }
        private string EncodeImageID(int id)
        {
            return _Hash.EncodingTxT(id.ToString());
        }
        public async Task UpdateImage(PutFileDTO file, ImageEntity image)
        {
            image.NormalPath = await _UploadImageFile.SaveFileAsync(file.FormFile);
            image.TinyPath = null;
            image.BigPath = null;
            image.Flag = true;
            image.CreatedOn = DateTime.Now;
            image.Name = file.FormFile.Name;
            image.Type = file.FormFile.ContentType;

            _ImageRepository.Update(image);
        }
        public void DeleteImage(ImageEntity deleteImage)
        {
            deleteImage.IsRemove = true;
            _ImageRepository.Update(deleteImage);
        }
    }
}
