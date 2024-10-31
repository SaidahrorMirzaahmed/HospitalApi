using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.ApiServices.Users;

public interface IUserApiService
{
    Task<UserViewModel> PostStaffAsync(StaffCreateModel createModel);
    Task<UserViewModel> PostClientAsync(UserCreateModel createModel);
    Task<UserViewModel> PutStaffAsync(long id, StaffUpdateModel createModel);
    Task<UserViewModel> PutClientAsync(long id, UserUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<UserViewModel>> GetAllClientAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<UserViewModel>> GetAllStaffAsync(PaginationParams @params, Filter filter, string search = null);
}