namespace HospitalApi.WebApi.Models.Users;

public class UserUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birth { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}