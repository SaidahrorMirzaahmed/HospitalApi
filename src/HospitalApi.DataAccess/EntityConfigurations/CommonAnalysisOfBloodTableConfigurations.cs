using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class CommonAnalysisOfBloodTableConfigurations : IEntityTypeConfiguration<CommonAnalysisOfBloodTable>
{
    public void Configure(EntityTypeBuilder<CommonAnalysisOfBloodTable> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany<CommonAnalysisOfBloodTableResult>()
            .WithOne()
            .HasForeignKey(x => x.CommonAnalysisOfBloodTableId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}