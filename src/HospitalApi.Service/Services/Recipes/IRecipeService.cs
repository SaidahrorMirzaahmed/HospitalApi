using HospitalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.Recipes;

public interface IRecipeService
{
    Task<Recipe> CreateAsync(Recipe recipe);
    Task<Recipe> UpdateAsync(long id, Recipe recipe);
    Task<bool> DeleteAsync(long id);
    Task<Recipe> GetAsync(long id);
    Task<IEnumerable<Recipe>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<Recipe>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}
