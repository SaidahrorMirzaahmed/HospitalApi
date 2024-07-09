using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Services.Users;
using Tenge.Service.Helpers;

namespace HospitalApi.WebApi.ApiServices.Accounts
{
    public class AccountApiService(IUserService service) : IAccountApiService
    {
        public async Task<bool> SendSMSCodeAsync(string phone)
        {
            var checker = ValidationHelper.IsPhoneValid(phone);
            if (!checker)
                throw new ArgumentIsNotValidException($"{nameof(phone)} is not valid");
            return await service.SendSMSCodeAsync(phone);
        }

        public async Task<(User user, string token)> VerifySMSCode(string phone, long code)
        {
            var checker = ValidationHelper.IsPhoneValid(phone);
            if (!checker)
                throw new ArgumentIsNotValidException($"{nameof(phone)} is not valid");
            return await service.VerifySMSCode(phone, code);
        }
    }
}
