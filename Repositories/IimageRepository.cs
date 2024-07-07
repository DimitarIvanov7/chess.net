using System.Net;
using WebApplication3.Model.Domain;

namespace WebApplication3.Repositories
{
    public interface IimageRepository
    {

        Task<Image> Upload(Image image);
        
    }
}
