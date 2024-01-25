using JendStore.Security.API.Configuration;
using JendStore.Security.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JendStore.Security.API.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
    
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApiUser> ApiUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.ApplyConfiguration(new RolesConfig());
        }

    }
}
