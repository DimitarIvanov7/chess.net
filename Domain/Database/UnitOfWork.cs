using WebApplication3.Domain.Abstractions.Data;
using WebApplication3.Domain.Database.DbContexts;

namespace WebApplication3.Domain.Database
{
    public class UnitOfWork: IUnitOfWork { 
    
        
            private readonly ApplicationDbContext context;

            public UnitOfWork(ApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task SaveChangesAsync()
            {
                await this.context.SaveChangesAsync();
            }
        
    }
}