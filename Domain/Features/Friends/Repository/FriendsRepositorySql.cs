using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Friends.Entities;

namespace WebApplication3.Domain.Features.Friends.Repository
{
    public class FriendsRepositorySql : GenericRepository<FriendsEntity>
    {
        public FriendsRepositorySql(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async override Task<List<FriendsEntity>>GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FriendsEntity>> GetListByIdAsync(Guid playerId, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var friendships = DbContext.Friends.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {


                if (filterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    friendships = friendships.Where(x => x.Id.Equals(playerId) && x.Status.Equals(filterQuery));
                }

            }

           
            int skipResults = (pageNumber - 1) * pageSize;

            return await friendships.Skip(skipResults).Take(pageSize).Include(g => g.PlayerOne).Include(g => g.PlayerTwo).ToListAsync();
        }


    }
}
