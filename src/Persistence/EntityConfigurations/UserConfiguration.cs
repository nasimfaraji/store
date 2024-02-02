using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(DomainConstants.UserNameMaxLength);

        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.Buyer)
            .HasForeignKey(x => x.UserId)
            .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}