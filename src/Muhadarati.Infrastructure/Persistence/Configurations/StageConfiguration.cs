using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Academic;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class StageConfiguration : IEntityTypeConfiguration<Stage>
{
    public void Configure(EntityTypeBuilder<Stage> builder)
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

        builder.HasIndex(s => new { s.DepartmentId, s.Number })
            .IsUnique();

        builder.HasMany(s => s.OwnedSubjects)
            .WithOne(sub => sub.OwningStage)
            .HasForeignKey(sub => sub.OwningStageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
