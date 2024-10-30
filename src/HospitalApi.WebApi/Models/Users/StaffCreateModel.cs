using HospitalApi.Domain.Enums;

namespace HospitalApi.WebApi.Models.Users;

public class StaffCreateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserSpecialists MedicalSpecialists { get; set; }
    public string Phone { get; set; }
}