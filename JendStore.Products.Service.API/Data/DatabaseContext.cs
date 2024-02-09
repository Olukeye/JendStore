using JendStore.Products.Service.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JendStore.Products.Service.API.Data
{
    public class DatabaseContext: DbContext
    {
        
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
