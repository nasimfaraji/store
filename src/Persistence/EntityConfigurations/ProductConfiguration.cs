using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasIndex(x => x.Title)
            .IsUnique();

        builder.Property(x => x.Title)
            .HasMaxLength(DomainConstants.ProductTitleMaxLength);
    }
}