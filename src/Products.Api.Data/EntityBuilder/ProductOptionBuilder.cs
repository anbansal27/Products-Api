using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Api.Entities;

namespace Products.Api.Data.EntityBuilder
{
    public static class ProductOptionBuilder
    {
        public static void Build(this EntityTypeBuilder<ProductOption> builder)
        {
            // columns                        
            builder.Property(po => po.Description).HasMaxLength(500);

            builder.Property(po => po.Name).HasColumnType("TEXT COLLATE NOCASE").HasMaxLength(255).IsRequired();
        }
    }
}
