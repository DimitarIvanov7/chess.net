using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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

        public PlayerController(IPlayerRepository playerRepository, IMapper mapper)
   
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<ActionResult> GetAllUsers([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string?  sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
        
            var playersDomain = await this.playerRepository.GetListAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);



            var playersDto = mapper.Map<List<PlayerResponseDto>>(playersDomain);


            return Ok(playersDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<ActionResult<PlayerResponseDto>> GetPlayerById([FromRoute] Guid id)
        {
            try
            {
                var playerDomain = await this.playerRepository.GetByIdAsync(id);

                if (playerDomain == null) return NotFound();


                var playersDto = mapper.Map<PlayerResponseDto>(playerDomain);

                return Ok(playersDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult<PlayerResponseDto>> CreatePlayer(AddPlayerDto player)
        {
            try
            {
                if (player == null)
                    return BadRequest();

                var playerDomainModel = mapper.Map<Player>(player);

                await this.playerRepository.CreateAsync(playerDomainModel);



                var createdPlayerDto = mapper.Map<PlayerResponseDto>(playerDomainModel);


                return CreatedAtAction(nameof(GetPlayerById),
                    new { id = createdPlayerDto.Id}, createdPlayerDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Player record");
            }
        }



        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult<PlayerResponseDto>> UpdatePlayer([FromRoute] Guid id, UpdatePlayerDto player)
        {
            try
            {
               if (player == null )
                {
                    return BadRequest();
                }

                var newPlayer = mapper.Map<Player>(player);

                var updatedPlayer = await this.playerRepository.UpdateAsync(id, newPlayer);

                if (updatedPlayer == null)
                    return NotFound();


                var playerDto = mapper.Map<PlayerResponseDto>(updatedPlayer);

                return Ok(playerDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Player record");
            }

        }



        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult> DeletePlayer([FromRoute] Guid id)
        {

            try { 

            var foundPlayer = await this.playerRepository.GetByIdAsync(id);


            if (foundPlayer == null) return NotFound();

             await this.playerRepository.Delete(foundPlayer);



            return Ok();


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Player record");
            }

        }
    }
}
