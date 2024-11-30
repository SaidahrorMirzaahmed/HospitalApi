using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class MedicalServiceTypeHistoryConfigurations : IEntityTypeConfiguration<MedicalServiceTypeHistory>
{
    public void Configure(EntityTypeBuilder<MedicalServiceTypeHistory> builder)
    {
        builder.HasKey(b => b.Id);

        builder
            .HasOne(b => b.Client)
            .WithMany()
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.MedicalServiceType)
            .WithMany()
            .HasForeignKey(b => b.MedicalServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(b => b.Client)
            .AutoInclude(true);

        builder
            .Navigation(b => b.MedicalServiceType)
            .AutoInclude(true);
    }
}