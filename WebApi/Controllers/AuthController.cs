using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Features.Auth.Dtos;
using WebApplication3.Domain.Features.Auth.Repository;
using WebApplication3.Model.DTO.PlayerDto;

namespace WebApplication3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepostitory;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepostitory, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.tokenRepostitory = tokenRepostitory;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AddPlayerDto player)
        {


            if (player == null)
                return BadRequest();

            var identityUser = new IdentityUser
            {
                UserName = player.UserName,
                Email = player.Email,
            };

            var identityResult = await userManager.CreateAsync(identityUser, player.Password);

            if (identityResult.Succeeded)
            {
                if (player.Roles != null && player.Roles.Any())
                {

                    identityResult = await userManager.AddToRolesAsync(identityUser, player.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("Register succesfull, please login!");
                    }

                }

            }

            return BadRequest("Something went wrong");

        }



        [HttpPost]
        [Route("Login")]

        //[SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(LoginResponseDto))]

        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginDto)
        {


            var user = await userManager.FindByNameAsync(loginDto.UserName);


            if (user != null)
            {

                var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkPassword)
                {

                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {

                        // CREATE token
                        var token = tokenRepostitory.createJWT(user, roles.ToList());

                        var response = new LoginResponseDto { JwtToken = token };

                        return Ok(response);
                    }

                }

            }

            return BadRequest("User or password is incorrect");

        }

    }
}
