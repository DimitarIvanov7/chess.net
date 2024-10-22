using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Domain.Features.Auth.Repository
{
    public interface ITokenRepository
    {


        string createJWT(IdentityUser user, List<string> roles);
    }
}
