using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
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
    IUserService service,
    IUnitOfWork unitOfWork) : IAccountApiService
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

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        }, out SecurityToken validatedToken);

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var phoneNumber = jwtToken.Claims.FirstOrDefault(c => c.Type == "Phone")?.Value;
        var userId = Convert.ToInt64(jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
        var user = await unitOfWork.Users.SelectAsync(entity => entity.Id == userId && entity.Phone == phoneNumber && !entity.IsDeleted)
            ?? throw new CustomException("Token is invalid", 403);

        return await Task.FromResult(true);
    }
}