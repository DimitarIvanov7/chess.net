using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class ChessDbContext : DbContext
    {

        public ChessDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }


        public DbSet<Players> Players { get; set; }

        public DbSet<GameType> GameTypes { get; set; }

        public DbSet<Games> Games { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Games and Players relationship
            modelBuilder.Entity<Games>()
                .HasOne(g => g.blackPlayer)
                .WithMany()
                .HasForeignKey(g => g.blackPlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Games>()
                .HasOne(g => g.whitePlayer)
                .WithMany()
                .HasForeignKey(g => g.whitePlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Games>()
                .HasOne(g => g.winner)
                .WithMany()
                .HasForeignKey(g => g.winnerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Games>()
                .HasOne(g => g.gameType)
                .WithMany()
                .HasForeignKey(g => g.gameTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }


}
