using HospitalApi.Domain.Enums;

namespace HospitalApi.WebApi.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
    public string Phone { get; set; }
}