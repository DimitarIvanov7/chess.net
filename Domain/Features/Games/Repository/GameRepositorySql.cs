using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Model.DTO;

namespace WebApplication3.Domain.Features.Games.Repository
{
    internal sealed class GameRepositorySql : Repository<GameEntity>
    {
        public GameRepositorySql(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<List<GameEntity>> GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var games = DbContext.Games.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {


                if (filterOn.Equals("GameType", StringComparison.OrdinalIgnoreCase))
                {
                    games = games.Where(x => x.GameType.Equals(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("GameTime", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAscending ? games.OrderBy(x => x.GameTime) : games.OrderByDescending(x => x.GameTime);

                }
                else if (sortBy.Equals("GameType", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAscending ? games.OrderBy(x => x.GameType) : games.OrderByDescending(x => x.GameType);


                }



            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await games.Skip(skipResults).Take(pageSize).ToListAsync();
        }













    }
}
