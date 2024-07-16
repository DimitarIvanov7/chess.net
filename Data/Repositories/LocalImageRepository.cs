using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication3.Data;
using WebApplication3.Model.Domain;

namespace WebApplication3.Data.Repositories
{
    public class LocalImageRepository : IimageRepository
    {

        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ChessDbContext chessDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnviroment, IHttpContextAccessor httpContextAccessor, ChessDbContext chessDbContext)
        {

            this.webHostEnviroment = webHostEnviroment;
            this.httpContextAccessor = httpContextAccessor;
            this.chessDbContext = chessDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnviroment.ContentRootPath, "Images",
                image.FileName);

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}";
            image.FilePath = urlFilePath;

            await chessDbContext.AddAsync(image);

            await chessDbContext.SaveChangesAsync();

            return image;





        }

    }
}
