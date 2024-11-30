using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class MedicalServiceTypeHistory : Auditable
{
    public int Queue { get; set; }

    public DateOnly QueueDate { get; set; }

    public long TicketId { get; set; }

    public long ClientId { get; set; }
    public User Client { get; set; }

    public long MedicalServiceTypeId { get; set; }
    public MedicalServiceType MedicalServiceType { get; set; }
}