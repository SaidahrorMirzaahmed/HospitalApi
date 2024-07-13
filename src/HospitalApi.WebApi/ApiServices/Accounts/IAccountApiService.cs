using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Models.Logins;

namespace HospitalApi.WebApi.ApiServices.Accounts
{
    public interface IAccountApiService
    {
        Task<bool> SendSMSCodeAsync(string phone);
        Task<LoginViewModel> VerifySMSCode(string phone, long code);
    }
}
