using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class BiochemicalAnalysisOfBloodTableConfigurations : IEntityTypeConfiguration<BiochemicalAnalysisOfBloodTableResult>
{
    public void Configure(EntityTypeBuilder<BiochemicalAnalysisOfBloodTableResult> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(result => result.BiochemicalAnalysisOfBloodTable)
            .WithMany(table => table.Items)
            .HasForeignKey(x => x.BiochemicalAnalysisOfBloodTableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}