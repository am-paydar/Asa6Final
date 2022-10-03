using Application.CommonServices.Hash;
using Application.CommonServices.UploadFile.Image;
using Application.CommonServices.UploadFile.Media;
using Application.ImageServices;
using Application.MediaServices;
using Domain.IRepository;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IUnitOfWork, MainContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IHash, Hash>();
builder.Services.AddScoped<IUploadImageFile, UploadImageFile>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IUploadMedia, UploadMedia>();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
