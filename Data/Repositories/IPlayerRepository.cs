using Microsoft.AspNetCore.Mvc;
using WebApplication3.Model.Domain;

namespace WebApplication3.Data.Repositories
{
    public interface IPlayerRepository
    {
        public Task<List<Player>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10);

        public Task<Player> GetByIdAsync(Guid id);

        public Task CreateAsync(Player player);

        public Task<Player> UpdateAsync(Guid id, Player player);


        public Task Delete(Player player);






    }
}
