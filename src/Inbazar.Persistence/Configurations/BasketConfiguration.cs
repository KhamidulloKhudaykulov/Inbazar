using Inbazar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inbazar.Persistence.Configurations;

public sealed class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Baskets");

        builder.HasKey(x => x.Id);

        builder.HasOne(b => b.User)
            .WithMany(u => u.Baskets)
            .HasForeignKey(b => b.UserId);

        builder.HasOne(b => b.Order)
            .WithOne(o => o.Basket)
            .HasForeignKey<Basket>(b => b.OrderId);

        builder.HasMany(b => b.OrderItems)
               .WithOne(oi => oi.Basket)
               .HasForeignKey(oi => oi.BasketId);

    }
}
