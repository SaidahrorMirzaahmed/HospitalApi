using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class CommonAnalysisOfBloodTableConfigurations : IEntityTypeConfiguration<CommonAnalysisOfBloodTableResult>
{
    public void Configure(EntityTypeBuilder<CommonAnalysisOfBloodTableResult> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(result => result.CommonAnalysisOfBloodTable)
            .WithMany(table => table.Items)
            .HasForeignKey(x => x.CommonAnalysisOfBloodTableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}