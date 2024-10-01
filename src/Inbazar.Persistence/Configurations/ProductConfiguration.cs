using Inbazar.Domain.Entities;
using Inbazar.Domain.ValueObjects.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inbazar.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.ProductCategories)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId);

        builder
            .Property(p => p.Name)
            .HasConversion(x => x!.Value, v => ProductName.Create(v).Value)
            .HasMaxLength(ProductName.Max_Lenght);

        builder
            .Property(p => p.Description)
            .HasConversion(x => x!.Value, v => Description.Create(v).Value)
            .HasMaxLength(Description.Max_Lenght);

        builder
            .Property(p => p.Price)
            .HasConversion(x => x!.Value, v => Price.Create(v).Value);
    }
}
