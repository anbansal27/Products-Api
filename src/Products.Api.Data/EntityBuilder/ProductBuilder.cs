using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Entities;

namespace Products.Api.Data.EntityBuilder
{
    public static class ProductBuilder
    {
        public static void Build(this EntityTypeBuilder<Product> builder)
        {
            // columns
            builder.Property(p => p.Code).HasColumnType("TEXT COLLATE NOCASE").HasMaxLength(50).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500);

            builder.Property(p => p.DeliveryPrice).HasColumnType("decimal(18,2)");

            builder.Property(p => p.Name).HasColumnType("TEXT COLLATE NOCASE").HasMaxLength(255).IsRequired();

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            // indexes
            builder.HasIndex(p => p.Name);

            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}