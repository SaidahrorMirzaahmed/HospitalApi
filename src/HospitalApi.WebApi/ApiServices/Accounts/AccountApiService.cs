using AutoMapper;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Logins;
using HospitalApi.WebApi.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HospitalApi.WebApi.ApiServices.Accounts;

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

    public async Task<bool> VerifyTokenAsync(string token)
    {
        var key = Encoding.UTF8.GetBytes(EnvironmentHelper.JWTKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);
        }
        catch (Exception)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}