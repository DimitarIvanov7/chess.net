using System.Net;
using WebApplication3.Model.Domain;

namespace WebApplication3.Data.Repositories
{
    public interface IimageRepository
    {

        Task<Image> Upload(Image image);

    }
}
