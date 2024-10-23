using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Images.Entities;

namespace WebApplication3.Domain.Features.Images.Repository
{
    public class LocalImageRepository : IimageRepository
    {

        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext applicationDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnviroment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
        {

            this.webHostEnviroment = webHostEnviroment;
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<ImageEntity> Upload(ImageEntity image)
        {
            var localFilePath = Path.Combine(webHostEnviroment.ContentRootPath, "Images",
                image.FileName);

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}";
            image.FilePath = urlFilePath;

            await applicationDbContext.AddAsync(image);

            await applicationDbContext.SaveChangesAsync();

            return image;





        }

    }
}
