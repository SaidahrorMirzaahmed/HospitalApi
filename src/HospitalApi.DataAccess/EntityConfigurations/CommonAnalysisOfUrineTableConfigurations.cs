using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class CommonAnalysisOfUrineTableConfigurations : IEntityTypeConfiguration<CommonAnalysisOfUrineTable>
{
    public void Configure(EntityTypeBuilder<CommonAnalysisOfUrineTable> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany<CommonAnalysisOfUrineTableResult>()
            .WithOne()
            .HasForeignKey(x => x.CommonAnalysisOfUrineTableId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}