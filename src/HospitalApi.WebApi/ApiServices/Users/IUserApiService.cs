using HospitalApi.WebApi.Models.Users;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Users;

public interface IUserApiService
{
    Task<UserViewModel> PostStaffAsync(UserCreateModel createModel);
    Task<UserViewModel> PostClientAsync(UserCreateModel createModel);
    Task<UserViewModel> PutAsync(long id, UserUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
