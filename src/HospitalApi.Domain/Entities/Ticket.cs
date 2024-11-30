using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Ticket : Auditable
{
    public bool IsPaid { get; set; }

    public bool IsCancelled { get; set; }

    public double CommonPrice { get; set; }

    public long ClientId { get; set; }
    public User Client { get; set; }

    public ICollection<MedicalServiceTypeHistory> MedicalServiceTypeHistories { get; set; }
}