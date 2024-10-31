using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.Users;
using HospitalApi.WebApi.Validations.Users;

namespace HospitalApi.WebApi.ApiServices.Users;

public class UserApiService(IMapper mapper,
    IUserService service,
    UserCreateModelValidator userCreateModelValidation,
    UserUpdateModelValidator userUpdateModelValidation,
    StaffCreateModelValidator staffCreateModelValidation,
    StaffUpdateModelValidator staffUpdateModelValidation) : IUserApiService
{
    public async Task<UserViewModel> PostClientAsync(UserCreateModel createModel)
    {
        await userCreateModelValidation.EnsureValidatedAsync(createModel);
        var createdUser = mapper.Map<User>(createModel);
        var res = await service.CreateUserAsync(createdUser);

        return mapper.Map<UserViewModel>(res);
    }

    public async Task<UserViewModel> PostStaffAsync(StaffCreateModel createModel)
    {
        await staffCreateModelValidation.EnsureValidatedAsync(createModel);
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


    public async Task<UserViewModel> PutStaffAsync(long id, StaffUpdateModel createModel)
    {
        await staffUpdateModelValidation.EnsureValidatedAsync(createModel);
        var mapperUser = mapper.Map<User>(createModel);
        var res = await service.UpdateStaffAsync(id, mapperUser);

        return mapper.Map<UserViewModel>(res);
    }

    public async Task<UserViewModel> PutClientAsync(long id, UserUpdateModel createModel)
    {
        await userUpdateModelValidation.EnsureValidatedAsync(createModel);
        var mapperUser = mapper.Map<User>(createModel);
        var res = await service.UpdateClientAsync(id, mapperUser);

        return mapper.Map<UserViewModel>(res);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllClientAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var result = await service.GetAllClientAsync(@params, filter, search);

        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllStaffAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var result = await service.GetAllStaffAsync(@params, filter, search);

        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }
}