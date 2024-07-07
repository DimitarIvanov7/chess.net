using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication3.Model.DTO;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PlayerController : Controller
    {


        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PlayerController> logger;

        public PlayerController(IPlayerRepository playerRepository, IMapper mapper, ILogger<PlayerController> logger)
   
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<ActionResult> GetAllUsers([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string?  sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

     

                logger.LogInformation("GetAllUser action method was envoked");

                var playersDomain = await playerRepository.GetListAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);



                var playersDto = mapper.Map<List<PlayerResponseDto>>(playersDomain);


                logger.LogInformation($"Finished getAllUsers method with data: {JsonSerializer.Serialize(playersDto)}");



                return Ok(playersDto);

          
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<ActionResult<PlayerResponseDto>> GetPlayerById([FromRoute] Guid id)
        {
            
                var playerDomain = await playerRepository.GetByIdAsync(id);

                if (playerDomain == null) return NotFound();


                var playersDto = mapper.Map<PlayerResponseDto>(playerDomain);

                return Ok(playersDto);
            
            
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<PlayerResponseDto>> CreatePlayer(AddPlayerDto player)
        {
           
                if (player == null)
                    return BadRequest();

                var playerDomainModel = mapper.Map<Player>(player);

                await playerRepository.CreateAsync(playerDomainModel);

                var createdPlayerDto = mapper.Map<PlayerResponseDto>(playerDomainModel);


                return CreatedAtAction(nameof(GetPlayerById),
                    new { id = createdPlayerDto.Id}, createdPlayerDto);
        }



        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult<PlayerResponseDto>> UpdatePlayer([FromRoute] Guid id, UpdatePlayerDto player)
        {
           
               if (player == null )
                {
                    return BadRequest();
                }

                var newPlayer = mapper.Map<Player>(player);

                var updatedPlayer = await playerRepository.UpdateAsync(id, newPlayer);

                if (updatedPlayer == null)
                    return NotFound();


                var playerDto = mapper.Map<PlayerResponseDto>(updatedPlayer);

            return Ok(playerDto);

        }



        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult> DeletePlayer([FromRoute] Guid id)
        {

            var foundPlayer = await this.playerRepository.GetByIdAsync(id);


            if (foundPlayer == null) return NotFound();

             await playerRepository.Delete(foundPlayer);

            return Ok();


        }
    }
}
