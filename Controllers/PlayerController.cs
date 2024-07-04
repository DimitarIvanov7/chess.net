using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
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
        public async Task<ActionResult> GetAllUsers()
        {
        
            var playersDomain = await this.playerRepository.ToListAsync();



            var playersDto = mapper.Map<List<PlayerDto>>(playersDomain);


            return Ok(playersDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById([FromRoute] Guid id)
        {
            try
            {
                var playerDomain = await this.playerRepository.GetByIdAsync(id);

                if (playerDomain == null) return NotFound();


                var playersDto = mapper.Map<PlayerDto>(playerDomain);

                return Ok(playersDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(AddPlayerDto player)
        {
            try
            {
                if (player == null)
                    return BadRequest();

                var playerDomainModel = mapper.Map<Player>(player);

                await this.playerRepository.AddAsync(playerDomainModel);



                var createdPlayerDto = mapper.Map<PlayerDto>(playerDomainModel);


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
        public async Task<ActionResult<PlayerDto>> UpdatePlayer([FromRoute] Guid id, UpdatePlayerDto player)
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


                var playerDto = mapper.Map<PlayerDto>(updatedPlayer);

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
        public async Task<ActionResult> DeletePlayer([FromRoute] Guid id)
        {

            try { 

            var foundPlayer = await this.playerRepository.GetByIdAsync(id);


            if (foundPlayer == null) return NotFound();

             await this.playerRepository.Remove(foundPlayer);



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
