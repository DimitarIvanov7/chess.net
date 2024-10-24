using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Features.Auth.Dtos;
using WebApplication3.Domain.Features.Auth.Repository;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Domain.Data.Repositories;

using WebApplication3.Model.DTO.PlayerDto;
using WebApplication3.Domain.Features.Players.Repository;
using System.Text.Json;

namespace WebApplication3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepostitory;
        private readonly IRepository<PlayerEntity> playersRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepostitory, IMapper mapper, IRepository<PlayerEntity> playersRepository, IUnitOfWork unitOfWork, ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.tokenRepostitory = tokenRepostitory;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.playersRepository = playersRepository;
            this.logger = logger;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AddPlayerDto player)
        {
            if (player == null)
                return BadRequest();

            var identityUser = new IdentityUser
            {
                UserName = player.Username,
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

                        //create player
                        var playerEntity = mapper.Map<PlayerEntity>(player);
                        playerEntity.Id = Guid.Parse(identityUser.Id);

                        playersRepository.Add(playerEntity);
                        await unitOfWork.SaveChangesAsync();
                        logger.LogInformation($"Finished register method with data: {JsonSerializer.Serialize(player)}");


                        return Ok($"Register succesfull, please login! {playerEntity.Id}");
                    }
                    else
                    {
                        return BadRequest("One or more roles provided do not exist.");

                    }

                }

            }

            return BadRequest("Something went wrong");

        }



        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginDto)
        {


            var user = await userManager.FindByNameAsync(loginDto.Username);


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
