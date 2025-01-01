using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder
            .HasMany(entity => entity.CheckUps)
            .WithOne()
            .HasForeignKey(entity => entity.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.Staff) // Assuming Booking has a navigation property User
            .WithMany() // Assuming User has a collection of Bookings
            .HasForeignKey(b => b.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(entity => entity.PdfDetails)
            .WithMany()
            .HasForeignKey(entity => entity.PdfDetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(entity => entity.Diagnosis)
            .WithMany()
            .HasForeignKey(entity => entity.DiagnosisId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}