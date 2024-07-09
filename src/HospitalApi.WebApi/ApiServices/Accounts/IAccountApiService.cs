using HospitalApi.Domain.Entities;

namespace HospitalApi.WebApi.ApiServices.Accounts
{
    public interface IAccountApiService
    {
        Task<bool> SendSMSCodeAsync(string phone);
        Task<(User user, string token)> VerifySMSCode(string phone, long code);
    }
}
