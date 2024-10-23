using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Domain.Features.Players.Entities;

namespace WebApplication3.Domain.Database.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Ignore<System.Globalization.CultureInfo>();

        // Configure the Games and Players relationship
        modelBuilder.Entity<GameEntity>()
            .HasOne(g => g.BlackPlayer)
            .WithMany()
            .HasForeignKey(g => g.BlackPlayerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<GameEntity>()
            .HasOne(g => g.WhitePlayer)
            .WithMany()
            .HasForeignKey(g => g.WhitePlayerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<GameEntity>()
            .HasOne(g => g.Winner)
            .WithMany()
            .HasForeignKey(g => g.WinnerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<GameEntity>()
            .HasOne(g => g.GameType)
            .WithMany()
            .HasForeignKey(g => g.GameTypeId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<FriendsEntity>()
            .HasOne(g => g.PlayerOne)
            .WithMany()
            .HasForeignKey(g => g.PlayerOneId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FriendsEntity>()
            .HasOne(g => g.PlayerTwo)
            .WithMany()
            .HasForeignKey(g => g.PlayerTwoId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<PlayerEntity> Players { get; set; }

    public DbSet<GameEntity> Games { get; set; }

    public DbSet<FriendsEntity> Friends { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

}
