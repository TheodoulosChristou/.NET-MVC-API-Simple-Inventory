using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleInventory.Models;

namespace SimpleInventory.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Sku).IsRequired();

            builder.Property(x => x.Sku).HasMaxLength(32);

            builder.Property(x=>x.Name).IsRequired();

            builder.Property(x => x.Price).HasPrecision(18, 2);
        }
    }
}
