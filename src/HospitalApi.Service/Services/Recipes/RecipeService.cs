using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Service.Services.Recipes;

public class RecipeService(IUnitOfWork unitOfWork) : IRecipeService
{
    public async Task<Recipe> CreateAsync(Recipe recipe, IEnumerable<long> ids)
    {
        recipe.Create();
        var createdRecipe = await unitOfWork.Recipes.InsertAsync(recipe);
        var laboratories = unitOfWork.Laboratories.SelectAsQueryable(laboratory => ids.Contains(laboratory.Id) && !laboratory.IsDeleted);
        foreach (var laboratory in laboratories)
        {
            laboratory.RecipeId = createdRecipe.Id;
        }
        recipe.CheckUps = (await unitOfWork.Laboratories.BulkUpdateAsync(laboratories)).ToList();

        return createdRecipe;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRecipe = await unitOfWork.Recipes.SelectAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException($"Recipe with this id is not found id = {id}");
        existRecipe.Delete();

        await unitOfWork.Recipes.DeleteAsync(existRecipe);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var res = unitOfWork.Recipes
            .SelectAsQueryable(expression: recipe => !recipe.IsDeleted, isTracked: false, includes: ["Staff", "Client", "CheckUps"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(recipe =>
                recipe.Client.FirstName.ToLower().Contains(search) || recipe.Client.LastName.ToLower().Contains(search));

        return await res.ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var res = unitOfWork.Recipes
            .SelectAsQueryable(expression: recipe => !recipe.IsDeleted && recipe.ClientId == id, isTracked: false, includes: ["Staff", "Client", "CheckUps"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(recipe =>
                recipe.Client.FirstName.ToLower().Contains(search) || recipe.Client.LastName.ToLower().Contains(search));

        return await Task.FromResult(res);
    }

    public async Task<Recipe> GetAsync(long id)
    {
        var existRecipe = await unitOfWork.Recipes.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client", "CheckUps"])
            ?? throw new NotFoundException($"Recipe with this id is not found id = {id}");

        return await Task.FromResult(existRecipe);
    }

    public async Task<Recipe> UpdateAsync(long id, Recipe recipe, IEnumerable<long> ids)
    {
        var existRecipe = await unitOfWork.Recipes.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client", "CheckUps"])
            ?? throw new NotFoundException($"Recipe with this id is not found id = {id}");

        existRecipe.Update();

        existRecipe.ClientId = recipe.ClientId;
        existRecipe.StaffId = recipe.StaffId;
        existRecipe.Complaints = recipe.Complaints;
        existRecipe.Diagnosis = recipe.Diagnosis;
        existRecipe.Recommendations = recipe.Recommendations;
        
        var laboratories = unitOfWork.Laboratories.SelectAsQueryable(laboratory => ids.Contains(laboratory.Id) && !laboratory.IsDeleted);
        foreach (var laboratory in existRecipe.CheckUps)
        {
            laboratory.RecipeId = null;
        }
        foreach (var laboratory in laboratories)
        {
            laboratory.RecipeId = existRecipe.Id;
        }


        await unitOfWork.SaveAsync();

        return existRecipe;
    }
}