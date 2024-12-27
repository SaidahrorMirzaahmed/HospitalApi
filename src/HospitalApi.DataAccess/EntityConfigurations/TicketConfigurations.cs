using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.DataAccess.EntityConfigurations;

public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(ticket => ticket.Id);

        builder
            .HasMany(b => b.MedicalServiceTypeHistories)
            .WithOne()
            .HasForeignKey(history => history.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(entity => entity.PdfDetails)
            .WithMany()
            .HasForeignKey(entity => entity.PdfDetailsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(b => b.Client)
            .AutoInclude(true);

        builder
            .Navigation(b => b.MedicalServiceTypeHistories)
            .AutoInclude(true);
    }
}