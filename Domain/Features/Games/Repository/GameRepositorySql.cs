using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Model.DTO;

namespace WebApplication3.Domain.Features.Games.Repository
{
    public class GameRepositorySql : GenericRepository<GameEntity>
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
                    games = games.Where(x => x.GameStateType.Equals(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("GameTime", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAscending ? games.OrderBy(x => x.CreatedDate) : games.OrderByDescending(x => x.CreatedDate);

                }
                else if (sortBy.Equals("GameType", StringComparison.OrdinalIgnoreCase))
                {
                    games = isAscending ? games.OrderBy(x => x.GameStateType) : games.OrderByDescending(x => x.GameStateType);

                }

            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await games.Skip(skipResults).Take(pageSize).Include(g => g.WhitePlayer).Include(g => g.WhitePlayer).ToListAsync();
        }


    }
}
