using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class CommonAnalysisOfUrineTableConfigurations : IEntityTypeConfiguration<CommonAnalysisOfUrineTableResult>
{
    public void Configure(EntityTypeBuilder<CommonAnalysisOfUrineTableResult> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(result => result.CommonAnalysisOfUrineTable)
            .WithMany(table => table.Items)
            .HasForeignKey(x => x.CommonAnalysisOfUrineTableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}