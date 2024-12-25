using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class LaboratoryConfigurations : IEntityTypeConfiguration<Laboratory>
{
    public void Configure(EntityTypeBuilder<Laboratory> builder)
    {
        builder
            .Property(entity => entity.RecipeId)
            .IsRequired(false);

        builder
            .HasOne(entity => entity.Client)
            .WithMany()
            .HasForeignKey(entity => entity.ClientId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(entity => entity.Staff)
            .WithMany()
            .HasForeignKey(entity => entity.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(entity => entity.PdfDetails)
            .WithMany()
            .HasForeignKey(entity => entity.PdfDetailsId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}