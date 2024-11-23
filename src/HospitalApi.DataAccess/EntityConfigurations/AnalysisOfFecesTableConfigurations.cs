using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class AnalysisOfFecesTableConfigurations : IEntityTypeConfiguration<AnalysisOfFecesTable>
{
    public void Configure(EntityTypeBuilder<AnalysisOfFecesTable> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany<AnalysisOfFecesTableResult>()
            .WithOne()
            .HasForeignKey(x => x.AnalysisOfFecesTableId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}