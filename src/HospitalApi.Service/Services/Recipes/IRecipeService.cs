using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Recipes;

public interface IRecipeService
{
    Task<Recipe> CreateAsync(Recipe recipe, IEnumerable<long> ids);
    Task<Recipe> UpdateAsync(long id, Recipe recipe, IEnumerable<long> ids);
    Task<bool> DeleteAsync(long id);
    Task<Recipe> GetAsync(long id);
    Task<IEnumerable<Recipe>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<Recipe>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}