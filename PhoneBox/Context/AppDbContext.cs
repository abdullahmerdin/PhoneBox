using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBox.Entities.Identity;

namespace PhoneBox.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Azure"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().Ignore(c => c.PhoneNumber)
                                           .Ignore(c => c.PhoneNumberConfirmed);
        }
    }
}
