using HospitalApi.Domain.Enums;

namespace HospitalApi.WebApi.Models.Users;

public class StaffUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserSpecialists MedicalSpecialists { get; set; }
    public string Phone { get; set; }
}