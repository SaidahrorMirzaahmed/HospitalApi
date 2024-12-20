using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class TorchTableConfigurations : IEntityTypeConfiguration<TorchTableResult>
{
    public void Configure(EntityTypeBuilder<TorchTableResult> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(result => result.TorchTable)
            .WithMany(table => table.Items)
            .HasForeignKey(x => x.TorchTableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}