using HospitalApi.Domain.Entities;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.Users;

public interface IUserService
{
    Task<User> CreateStaffAsync(User user);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateAsync(long id, User user);
    Task<bool> DeleteAsync(long id);
    Task<User> GetByIdAsync(long id);
    Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<bool> SendSMSCodeAsync(string phone);
    Task<(User user, string token)> VerifySMSCode(string phone, long code);
}