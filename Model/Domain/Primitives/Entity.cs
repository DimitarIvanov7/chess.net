namespace Domain.Primitives;

public abstract class Entity : IEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; set; }

    
    protected Entity(Guid id)
    {
        Id = id;
        
    }

    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
