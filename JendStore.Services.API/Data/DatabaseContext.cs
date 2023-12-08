using JendStore.Services.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JendStore.Services.API.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options)
        {
          
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
