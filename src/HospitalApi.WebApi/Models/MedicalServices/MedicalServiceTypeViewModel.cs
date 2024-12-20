using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.MedicalServices;

public class MedicalServiceTypeViewModel
{
    public long Id { get; set; }

    public string ServiceTypeTitle { get; set; }
    public string ServiceTypeTitleRu { get; set; }

    public double Price { get; set; }

    public ClinicQueueViewModel ClinicQueue { get; set; }

    public long StaffId { get; set; }
    public UserViewModel Staff { get; set; }
}