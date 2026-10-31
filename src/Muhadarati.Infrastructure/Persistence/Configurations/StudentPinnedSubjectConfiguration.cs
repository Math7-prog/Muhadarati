using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhadarati.Domain.Students;
using Muhadarati.Infrastructure.Identity;

namespace Muhadarati.Infrastructure.Persistence.Configurations;

public class StudentPinnedSubjectConfiguration : IEntityTypeConfiguration<StudentPinnedSubject>
{
    public void Configure(EntityTypeBuilder<StudentPinnedSubject> builder)
    {
        builder.HasKey(p => new { p.UserId, p.SubjectId });

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Subject)
            .WithMany()
            .HasForeignKey(p => p.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
