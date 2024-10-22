﻿using Microsoft.EntityFrameworkCore;
using System.Xml;
using WebApplication3.Domain.Data.Repositories;
using WebApplication3.Domain.Database.DbContexts;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Model.DTO;

namespace WebApplication3.Domain.Features.Games.Repository
{
    internal sealed class GameRepositorySql : Repository<PlayerEntity>
    {
        public GameRepositorySql(ApplicationDbContext dbContext) : base(dbContext)
        {

        }


        public async Task<List<PlayerEntity>> GetListAsync<Entity>(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var players = DbContext.Players.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {


                if (filterOn.Equals("Username", StringComparison.OrdinalIgnoreCase))
                {
                    players = players.Where(x => x.Username.Contains(filterQuery));
                }



            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {

                if (sortBy.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    players = isAscending ? players.OrderBy(x => x.Username) : players.OrderByDescending(x => x.Username);

                }
                else if (sortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    players = isAscending ? players.OrderBy(x => x.Email) : players.OrderByDescending(x => x.Email);


                }



            }


            int skipResults = (pageNumber - 1) * pageSize;

            return await players.Skip(skipResults).Take(pageSize).ToListAsync();
        }













    }
}
