using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Data;
using WebApplication3.Model.Domain;
using WebApplication3.Model.DTO;

namespace WebApplication3.Data.Repositories
{
    public class GameRepositorySql
    {
        private readonly ChessDbContext dbContext;


        public GameRepositorySql(ChessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task Save()
        {

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Player>> GetListAsync<Entity>(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {

            var players = dbContext.Players.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {


                if (filterOn.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    players = players.Where(x => x.UserName.Contains(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    players = isAscending ? players.OrderBy(x => x.UserName) : players.OrderByDescending(x => x.UserName);

                }
                else if (sortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    players = isAscending ? players.OrderBy(x => x.Email) : players.OrderByDescending(x => x.Email);


                }



            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await players.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Player> GetByIdAsync(Guid id)
        {

            var found = await dbContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            if (found == null) return null;

            return found;
        }



        public async Task CreateAsync(Player playerDomainModel)
        {

            await dbContext.AddAsync(playerDomainModel);

            await Save();
        }


        public async Task Delete(Player player)
        {
            dbContext.Remove(player);
            await Save();

        }


        public async Task<Player> UpdateAsync(Guid id, Player player)
        {

            var foundPlayer = await GetByIdAsync(id);

            if (foundPlayer == null) return null;


            foundPlayer.Email = player.Email;
            foundPlayer.Password = player.Password;
            foundPlayer.UserName = player.UserName;

            await Save();

            return foundPlayer;


        }







    }
}
