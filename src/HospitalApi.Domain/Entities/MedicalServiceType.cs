using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class MedicalServiceType : Auditable
{
    public string ServiceTypeTitle { get; set; }
    public string ServiceTypeTitleRu { get; set; }

    public double Price { get; set; }

    public ClinicQueue ClinicQueue { get; set; }

    public long StaffId { get; set; }
    public User Staff { get; set; }
}