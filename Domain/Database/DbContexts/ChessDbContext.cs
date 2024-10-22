using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Images.Entities;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Model.Domain.Games.Entities;

namespace WebApplication3.Domain.Database.DbContexts
{
    public class ChessDbContext : DbContext
    {

        public ChessDbContext(DbContextOptions<ChessDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }


        public DbSet<PlayerEntity> Players { get; set; }

        public DbSet<GameTypeEntity> GameType { get; set; }

        public DbSet<GameEntity> Games { get; set; }

        public DbSet<ImageEntity> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }


    }


}
