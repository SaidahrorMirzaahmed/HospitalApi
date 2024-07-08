using HospitalApi.Service.Services.Users;

namespace HospitalApi.WebApi.ApiServices.Accounts
{
    public interface IAccountApiService
    {

    }

    public class AccountApiService(IUserService service) : IAccountApiService 
    { 
        
    }
}
