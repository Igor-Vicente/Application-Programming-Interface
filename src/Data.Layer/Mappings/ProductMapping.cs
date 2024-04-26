using Business.Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Layer.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(200)");
            builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(2000)");
            builder.ToTable("Products");
        }
    }
}
/*
 * nvarchar accepts special characters
 */