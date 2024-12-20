using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class AnalysisOfFecesTableConfigurations : IEntityTypeConfiguration<AnalysisOfFecesTableResult>
{
    public void Configure(EntityTypeBuilder<AnalysisOfFecesTableResult> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(result => result.AnalysisOfFecesTable)
            .WithMany(table => table.Items)
            .HasForeignKey(result => result.AnalysisOfFecesTableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}