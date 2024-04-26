using Business.Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Layer.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasColumnType("varchar(200)");
            builder.Property(s => s.Document).IsRequired().HasColumnType("varchar(14)");

            /******************** 1 : 1 relation ******************/
            builder.HasOne(s => s.Address).WithOne(a => a.Supplier);

            /******************** 1 : N relation ******************/
            builder.HasMany(s => s.Products).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId);


            builder.ToTable("Suppliers");
        }
    }
}
