using WebApplication3.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Thread = WebApplication3.Model.Domain.Thread;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Player> Players { get; set; }

    DbSet<Game> Games { get; set; }

    DbSet<Friends> Friends { get; set; }

    DbSet<Message> Messages { get; set; }

    DbSet<Thread> Threads { get; set; }
}
