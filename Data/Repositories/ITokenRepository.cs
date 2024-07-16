using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Data.Repositories
{
    public interface ITokenRepository
    {


        string createJWT(IdentityUser user, List<string> roles);
    }
}
