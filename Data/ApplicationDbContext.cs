using Application.Data;
using Domain.Primitives;
using MediatR;
using WebApplication3.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Thread = WebApplication3.Model.Domain.Thread;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
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

    public DbSet<Player> Players { get; set; }

    public     DbSet<Game> Games { get; set; }

    public     DbSet<Friends> Friends { get; set; }

    public     DbSet<Message> Messages { get; set; }

    public     DbSet<Thread> Threads { get; set; }

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
