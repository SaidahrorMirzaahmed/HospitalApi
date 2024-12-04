using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class MedicalServiceTypeConfigurations : IEntityTypeConfiguration<MedicalServiceType>
{
    public void Configure(EntityTypeBuilder<MedicalServiceType> builder)
    {
        builder.HasKey(b => b.Id);

        builder
            .HasOne(b => b.Staff)
            .WithMany()
            .HasForeignKey(b => b.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(b => b.Staff)
            .AutoInclude(true);

        builder
            .HasOne(b => b.ClinicQueue)
            .WithOne(b => b.MedicalServiceType)
            .HasForeignKey<ClinicQueue>(b => b.MedicalServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(b => b.ClinicQueue)
            .AutoInclude(true);
    }
}