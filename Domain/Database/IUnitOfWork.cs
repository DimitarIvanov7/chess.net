namespace WebApplication3.Domain.Abstractions.Data;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
