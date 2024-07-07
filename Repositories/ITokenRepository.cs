using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Repositories
{
    public interface ITokenRepository
    {


        string createJWT(IdentityUser user, List<string> roles);
    }
}
