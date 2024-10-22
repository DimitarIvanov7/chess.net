using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Features.Friends.Entities;
using WebApplication3.Domain.Features.Games.Entities;
using WebApplication3.Domain.Features.Messages.Entities;
using WebApplication3.Domain.Features.Players.Entities;
using WebApplication3.Domain.Features.Threads.Entities;

namespace WebApplication3.Domain.Database.DbContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<PlayerEntity> Players { get; set; }

    public DbSet<GameEntity> Games { get; set; }

    public DbSet<FriendsEntity> Friends { get; set; }

    public DbSet<MessageEntity> Messages { get; set; }

    public DbSet<ThreadEntity> Threads { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents();

                e.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
