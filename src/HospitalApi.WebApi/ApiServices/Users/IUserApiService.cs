using HospitalApi.WebApi.Models.Users;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Users;

public interface IUserApiService
{
    ValueTask<UserViewModel> PostStaffAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> PostClientAsync(UserCreateModel createModel);
    ValueTask<UserViewModel> PutAsync(long id, UserUpdateModel createModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
