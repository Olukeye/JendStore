using JendStore.Cart.Service.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JendStore.Cart.Service.API.Data
{
    public class DatabaseContext: DbContext
    {
        
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
