using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Database;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Model.DTO;

namespace WebApplication3.Domain.Features.Messages.Repository
{
    public class MessagesRepositorySql : GenericRepository<MessageEntity>
    {
        public MessagesRepositorySql(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MessageEntity>> GetListByIdsAsync(Guid filterQuery, string? filterOn = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var messages = DbContext.Messages.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false)
            {


                if (filterOn.Equals("ReceiverId", StringComparison.OrdinalIgnoreCase))
                {
                    messages = messages.Where(x => x.ReceiverId.Equals(filterQuery));
                }

                if (filterOn.Equals("SenderId", StringComparison.OrdinalIgnoreCase))
                {
                    messages = messages.Where(x => x.SenderId.Equals(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("SendDate", StringComparison.OrdinalIgnoreCase))
                {
                    messages = isAscending ? messages.OrderBy(x => x.SendDate) : messages.OrderByDescending(x => x.SendDate);

                }


            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await messages.Skip(skipResults).Take(pageSize).Include(g => g.Sender).Include(g => g.Receiver).ToListAsync();
        }

        public async override Task<List<MessageEntity>>GetListAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var messages = DbContext.Messages.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {


                if (filterOn.Equals("ReceiverId", StringComparison.OrdinalIgnoreCase))
                {
                    messages = messages.Where(x => x.ReceiverId.Equals(filterQuery));
                }

                if (filterOn.Equals("SenderId", StringComparison.OrdinalIgnoreCase))
                {
                    messages = messages.Where(x => x.SenderId.Equals(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("SendDate", StringComparison.OrdinalIgnoreCase))
                {
                    messages = isAscending ? messages.OrderBy(x => x.SendDate) : messages.OrderByDescending(x => x.SendDate);

                }
                

            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await messages.Skip(skipResults).Take(pageSize).Include(g => g.Sender).Include(g => g.Receiver).ToListAsync();
        }


    }
}
