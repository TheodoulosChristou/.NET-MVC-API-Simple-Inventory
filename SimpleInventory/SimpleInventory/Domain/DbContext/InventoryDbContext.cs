using Microsoft.EntityFrameworkCore;
using SimpleInventory.Domain.Configuration;
using SimpleInventory.Domain.Models;

public class InventoryDbContext : DbContext
{
            
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product {  get; set; }

        public DbSet<Category> Category { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
}