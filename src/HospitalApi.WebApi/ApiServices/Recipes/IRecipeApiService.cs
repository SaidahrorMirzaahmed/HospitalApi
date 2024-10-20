﻿using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.Recipes;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Recipes;

public interface IRecipeApiService
{
    Task<RecipeViewModel> PostAsync(RecipeCreateModel createModel);
    Task<RecipeViewModel> PutAsync(long id, RecipeUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<RecipeViewModel> GetAsync(long id);
    Task<IEnumerable<RecipeViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<RecipeViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}
