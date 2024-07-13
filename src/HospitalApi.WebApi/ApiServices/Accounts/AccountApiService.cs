using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.Models.Logins;
using HospitalApi.WebApi.Models.Users;
using Tenge.Service.Helpers;

namespace HospitalApi.WebApi.ApiServices.Accounts
{
    public class AccountApiService(
        IMapper mapper,
        IUserService service) : IAccountApiService
    {
        public async Task<bool> SendSMSCodeAsync(string phone)
        {
            var checker = ValidationHelper.IsPhoneValid(phone);
            if (!checker)
                throw new ArgumentIsNotValidException($"{nameof(phone)} is not valid");
            return await service.SendSMSCodeAsync(phone);
        }

        public async Task<LoginViewModel> VerifySMSCode(string phone, long code)
        {
            var checker = ValidationHelper.IsPhoneValid(phone);
            if (!checker)
                throw new ArgumentIsNotValidException($"{nameof(phone)} is not valid");
            var result = await service.VerifySMSCode(phone, code);
            var final = new LoginViewModel()
            {
                User = mapper.Map<UserViewModel>(result.user),
                Token = result.token,
            };
            return final;
        }
    }
}
