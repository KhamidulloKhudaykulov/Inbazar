using Inbazar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inbazar.Persistence.Configurations;

public sealed class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(c => c.Id);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired();

        builder.HasOne(p => p.Product)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.ProductId)
            .IsRequired();
    }
}
