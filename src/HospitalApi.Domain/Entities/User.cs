using HospitalApi.Domain.Commons;
using HospitalApi.Domain.Enums;

namespace HospitalApi.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public UserSpecialists MedicalSpecialists { get; set; }
    public UserRole Role { get; set; }

}