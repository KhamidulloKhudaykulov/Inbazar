using Inbazar.Domain.Entities;
using Inbazar.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inbazar.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder
            .Property(user => user.FirstName)
            .HasConversion(x => x!.Value, v => FirstName.Create(v).Value)
            .HasMaxLength(FirstName.MaxLenght);

        builder
            .Property(user => user.LastName)
            .HasConversion(x => x!.Value, v => LastName.Create(v).Value)
            .HasMaxLength(LastName.MaxLenght);

        builder
            .Property(user => user.Email)
            .HasConversion(x => x!.Value, v => Email.Create(v).Value);

        builder.Property(user => user.Phone).IsRequired();

        builder.HasMany(user => user.Orders)
            .WithOne(order => order.User)
            .HasForeignKey(order => order.UserId);
    }
}
