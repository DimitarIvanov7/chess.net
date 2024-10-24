using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Features.Games.Dtos;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Players.Entities;


namespace WebApplication3.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IRepository<GameEntity> gamesRepository;
        private readonly IRepository<PlayerEntity> playersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GamesController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly GamesHelpers gamesHelpers;

        public GamesController(IRepository<GameEntity> gamesRepository, IRepository<PlayerEntity> playersRepository, IMapper mapper, ILogger<GamesController> logger, IUnitOfWork unitOfWork, GamesHelpers gamesHelpers)

        {
            this.gamesRepository = gamesRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.playersRepository = playersRepository;
            this.gamesHelpers = gamesHelpers;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllGames([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {


            var gamesDomain = await gamesRepository.GetListAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            var gamesDto = mapper.Map<List<GameResponseDto>>(gamesDomain);


            logger.LogInformation($"Finished getAllGames method with data: {JsonSerializer.Serialize(gamesDto)}");

            return Ok(gamesDto);


        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<ActionResult<GameResponseDto>> GetGameById([FromRoute] Guid id)
        {

            var gameDomain = await gamesRepository.GetByIdAsync(id);

            if (gameDomain == null) return NotFound();


            var gameDto = mapper.Map<GameResponseDto>(gameDomain);

            logger.LogInformation($"Finished getGame method with data: {JsonSerializer.Serialize(gameDto)}");


            return Ok(gameDto);


        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<ActionResult<GameResponseDto>> CreateGame(AddGameDto game)
        {

            if (game == null)
                return BadRequest();

            

            var foundPlayer = await playersRepository.GetByIdAsync(game.WhitePlayerId);

            if(foundPlayer == null)
            {
                return BadRequest("No such player");
            }


            bool isPlayerInGame = await gamesHelpers.IsPlayerInRunningGameAsync(foundPlayer.Id);

            if(isPlayerInGame)
            {
                return BadRequest("Player is already in a game");
            }


            var gameDomainModel = mapper.Map<GameEntity>(game);
            gameDomainModel.WhitePlayer = foundPlayer;
            gameDomainModel.WhitePlayerId = foundPlayer.Id;

            gamesRepository.Add(gameDomainModel);

            await unitOfWork.SaveChangesAsync();

            var createdGameDto = mapper.Map<GameResponseDto>(gameDomainModel);

            return CreatedAtAction(nameof(GetGameById),
                new { id = createdGameDto.Id }, createdGameDto);
        }



        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]

        public async Task<ActionResult<GameResponseDto>> UpdateGame([FromRoute] Guid id, UpdateGameDto game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            // Check if the black player exists
            if (game.BlackPlayer.HasValue)
            {
                var foundBlackPlayer = await playersRepository.GetByIdAsync(game.BlackPlayer.Value);
                if (foundBlackPlayer == null)
                {
                    return BadRequest("No such player");
                }

                bool isInOtherGames = await gamesHelpers.IsPlayerInOtherRunningGameAsync(foundBlackPlayer.Id, id);

                if(isInOtherGames)
                {
                    return BadRequest("Player is already in a game");
                }

            }

            GameEntity newGame = mapper.Map<GameEntity>(game);
            newGame.Id = id;

            gamesRepository.Update(newGame);
            await unitOfWork.SaveChangesAsync();

            var gameDto = mapper.Map<GameResponseDto>(newGame);

            return Ok(gameDto);

        }



        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<ActionResult> DeleteGame([FromRoute] Guid id)
        {

            var foundGame = await gamesRepository.GetByIdAsync(id);


            if (foundGame == null) return NotFound();

            gamesRepository.Remove(foundGame);

            await unitOfWork.SaveChangesAsync();

            return Ok();


        }
    }
}
