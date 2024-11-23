using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class BiochemicalAnalysisOfBloodTableConfigurations : IEntityTypeConfiguration<BiochemicalAnalysisOfBloodTable>
{
    public void Configure(EntityTypeBuilder<BiochemicalAnalysisOfBloodTable> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany<BiochemicalAnalysisOfBloodTableResult>()
            .WithOne()
            .HasForeignKey(x => x.BiochemicalAnalysisOfBloodTableId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}