using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Domain.Database.DbContexts
{
    public class AuthDbContext : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //just testing
            base.OnModelCreating(modelBuilder);

            string readerRoleId = "tes317d9700-5ef6-44b8-b775-2598fdca1690";

            string writerRoleId = "3aee525e-3305-43e7-87a7-6af812d11883";


            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                     Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                },



            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
