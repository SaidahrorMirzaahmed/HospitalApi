﻿using HospitalApi.WebApi.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Users;

public interface IUserApiService
{
    Task<UserViewModel> PostStaffAsync(UserCreateModel createModel);
    Task<UserViewModel> PostClientAsync(UserCreateModel createModel);
    Task<UserViewModel> PutStaffAsync(long id, UserUpdateModel createModel);
    Task<UserViewModel> PutClientAsync(long id, UserUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<UserViewModel>> GetAllClientAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<UserViewModel>> GetAllStaffAsync(PaginationParams @params, Filter filter, string search = null);
}
