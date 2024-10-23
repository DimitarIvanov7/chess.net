using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Domain.Features.Messages.Entities;


namespace WebApplication3.Domain.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<PlayerEntity> Players { get; set; }

    DbSet<GameEntity> Games { get; set; }

    DbSet<FriendsEntity> Friends { get; set; }

    DbSet<MessageEntity> Messages { get; set; }

}
