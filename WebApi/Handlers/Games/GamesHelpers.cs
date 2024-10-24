using WebApplication3.Domain.Constants;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Players.Entities;

public class GamesHelpers
{
    private readonly IRepository<PlayerEntity> playersRepository;
    private readonly IRepository<GameEntity> gamesRepository;

    public GamesHelpers(IRepository<PlayerEntity> playersRepository, IRepository<GameEntity> gamesRepository)
    {
        this.playersRepository = playersRepository;
        this.gamesRepository = gamesRepository;
    }

    public async Task<bool> IsPlayerInRunningGameAsync(Guid playerId)
    {
        var existingGamesAsWhite = await gamesRepository.GetListAsync(
            filterOn: nameof(GameEntity.WhitePlayerId),
            filterQuery: playerId.ToString()
        );

        var existingGamesAsBlack = await gamesRepository.GetListAsync(
            filterOn: nameof(GameEntity.BlackPlayerId),
            filterQuery: playerId.ToString()
        );

        return existingGamesAsWhite.Any(g => !(g.GameStateType == PlayStateTypes.Winner || g.GameStateType == PlayStateTypes.Draw)) ||
               existingGamesAsBlack.Any(g => !(g.GameStateType == PlayStateTypes.Winner || g.GameStateType == PlayStateTypes.Draw));
    }


    public async Task<bool> IsPlayerInOtherRunningGameAsync(Guid playerId, Guid gameId)
    {
        var existingGamesAsWhite = await gamesRepository.GetListAsync(
            filterOn: nameof(GameEntity.WhitePlayerId),
            filterQuery: playerId.ToString()
        );

        var existingGamesAsBlack = await gamesRepository.GetListAsync(
            filterOn: nameof(GameEntity.BlackPlayerId),
            filterQuery: playerId.ToString()
        );

        return existingGamesAsWhite.Any(g => gameId != g.Id && !(g.GameStateType == PlayStateTypes.Winner || g.GameStateType == PlayStateTypes.Draw)) ||
               existingGamesAsBlack.Any(g => gameId != g.Id && !(g.GameStateType == PlayStateTypes.Winner || g.GameStateType == PlayStateTypes.Draw));
    }
}