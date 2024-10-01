using Inbazar.Domain.Entities;
using Inbazar.Domain.ValueObjects.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inbazar.Persistence.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.ProductCategories)
            .WithOne(pc => pc.Category)
            .HasForeignKey(pc => pc.CategoryId);

        builder
            .Property(c => c.Name)
            .HasConversion(x => x!.Value, v => CategoryName.Create(v).Value)
            .HasMaxLength(CategoryName.MaxLenght);
    }
}
