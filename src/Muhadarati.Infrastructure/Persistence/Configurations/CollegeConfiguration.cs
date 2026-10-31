using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.NameAr)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.NameEn)
            .HasMaxLength(200);

        builder.Property(c => c.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(c => c.Departments)
            .WithOne(d => d.College)
            .HasForeignKey(d => d.CollegeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
