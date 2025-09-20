using Microsoft.EntityFrameworkCore;
using SimpleInventory.Configuration;
using SimpleInventory.Models;


public class InventoryDbContext : DbContext
{
            
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
}