using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleInventory.Domain.Models;

namespace SimpleInventory.Domain.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Sku).IsRequired();

            builder.Property(x => x.Sku).HasMaxLength(32);

            builder.HasIndex(x => x.Sku).IsUnique();

            builder.Property(x=>x.Name).IsRequired();

            builder.Property(x => x.Price).HasPrecision(18, 2);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
