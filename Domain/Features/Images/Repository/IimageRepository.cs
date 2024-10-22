using System.Net;
using WebApplication3.Domain.Features.Images.Entities;

namespace WebApplication3.Domain.Features.Images.Repository
{
    public interface IimageRepository
    {

        Task<ImageEntity> Upload(ImageEntity image);

    }
}
