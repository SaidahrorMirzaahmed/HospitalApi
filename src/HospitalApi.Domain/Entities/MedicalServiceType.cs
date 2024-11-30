using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class MedicalServiceType : Auditable
{
    public string ServiceType { get; set; }

    public double Price { get; set; }

    public int LastQueue { get; set; }

    public DateOnly QueueDate { get; set; }

    public long StaffId { get; set; }
    public User Staff { get; set; }
}