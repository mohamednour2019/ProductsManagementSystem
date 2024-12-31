

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementSystem.Domain.Product.Entity;

namespace ProductManagementSystem.InfraStructure.EntitiesConfigurations
{
    public class ProductEntityConfigurations : IEntityTypeConfiguration<Domain.Product.Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey("Id");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("datetime2").IsRequired();
        }
    }
}
