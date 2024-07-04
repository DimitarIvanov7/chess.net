using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Data;
using WebApplication3.Model.DTO;

namespace WebApplication3.Repositories
{
    public class PlayerRepositoryMySql: IPlayerRepository
    {
        private readonly ChessDbContext dbContext;


        public PlayerRepositoryMySql(ChessDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task Save()
        {

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Player>> ToListAsync() {

            return await this.dbContext.Player.ToListAsync();


        }

        public async Task<Player> GetByIdAsync(Guid id) {

            var found =  await this.dbContext.Player.FirstOrDefaultAsync(x => x.Id  == id);

            if (found == null) return null;

            return found;
        }



        public async Task AddAsync(Player playerDomainModel) {

            await dbContext.AddAsync(playerDomainModel);

            await Save();
        }


        public async Task Remove(Player player) {
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
