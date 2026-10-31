using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.NameAr)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.NameEn)
            .HasMaxLength(200);

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(s => s.Lectures)
            .WithOne(l => l.Subject)
            .HasForeignKey(l => l.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
