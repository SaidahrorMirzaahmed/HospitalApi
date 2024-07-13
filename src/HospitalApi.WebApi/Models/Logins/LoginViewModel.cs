using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Logins
{
    public class LoginViewModel
    {
        public UserViewModel User { get; set; }
        public string Token {  get; set; }
    }
}
