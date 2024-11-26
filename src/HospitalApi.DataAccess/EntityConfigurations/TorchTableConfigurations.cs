using HospitalApi.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class TorchTableConfigurations : IEntityTypeConfiguration<TorchTable>
{
    public void Configure(EntityTypeBuilder<TorchTable> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany<TorchTableResult>()
            .WithOne()
            .HasForeignKey(x => x.TorchTableId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}