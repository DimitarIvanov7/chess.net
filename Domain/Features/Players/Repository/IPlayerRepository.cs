using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Features.Players.Repository
{
    public interface IPlayerRepository
    {

        Task<List<PlayerEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);



    }
}
