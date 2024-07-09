using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.Users;
using HospitalApi.WebApi.Validations.Users;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Users;

public class UserApiService(IMapper mapper,
    IUserService service,
    UserCreateModelValidator validations,
    UserUpdateModelValidator validations1) : IUserApiService
{
    public async Task<UserViewModel> PostClientAsync(UserCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        var createdUser = mapper.Map<User>(createModel);
        var res = await service.CreateUserAsync(createdUser);

        return mapper.Map<UserViewModel>(res);
    }

    public async Task<UserViewModel> PostStaffAsync(UserCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        var createdUser = mapper.Map<User>(createModel);
        var res = await service.CreateStaffAsync(createdUser);

        return mapper.Map<UserViewModel>(res);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await service.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var res = await service.GetAllAsync(@params, filter, search);
        var result = mapper.Map<IEnumerable<UserViewModel>>(res);

        return result;
    }

    public async Task<UserViewModel> GetAsync(long id)
    {
        var user = await service.GetByIdAsync(id);

        return mapper.Map<UserViewModel>(user);
    }


    public async Task<UserViewModel> PutAsync(long id, UserUpdateModel createModel)
    {
        await validations1.EnsureValidatedAsync(createModel);
        var mapperUser = mapper.Map<User>(createModel);
        var res = await service.UpdateAsync(id, mapperUser);

        return mapper.Map<UserViewModel>(res);
    }
}