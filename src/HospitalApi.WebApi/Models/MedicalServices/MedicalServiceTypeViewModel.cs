using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.MedicalServices;

public class MedicalServiceTypeViewModel
{
    public long Id { get; set; }

    public string ServiceType { get; set; }

    public double Price { get; set; }

    public int LastQueue { get; set; }

    public long StaffId { get; set; }
    public UserViewModel Staff { get; set; }
}