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
    public async Task<Recipe> CreateAsync(Recipe recipe)
    {
        recipe.Create();
        var createdRecipe = await unitOfWork.Recipes.InsertAsync(recipe);

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
            .SelectAsQueryable(expression: recipe => !recipe.IsDeleted, isTracked: false, includes: ["Staff", "Client", "Picture"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(recipe =>
                recipe.Client.FirstName.ToLower().Contains(search) || recipe.Client.LastName.ToLower().Contains(search));

        return await res.ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var res = unitOfWork.Recipes
            .SelectAsQueryable(expression: recipe => !recipe.IsDeleted && recipe.ClientId == id, isTracked: false, includes: ["Staff", "Client", "Picture"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(recipe =>
                recipe.Client.FirstName.ToLower().Contains(search) || recipe.Client.LastName.ToLower().Contains(search));

        return await Task.FromResult(res);
    }

    public async Task<Recipe> GetAsync(long id)
    {
        var existRecipe = await unitOfWork.Recipes.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client", "Picture"])
            ?? throw new NotFoundException($"Recipe with this id is not found id = {id}");

        return await Task.FromResult(existRecipe);
    }

    public async Task<Recipe> UpdateAsync(long id, Recipe recipe)
    {
        var existRecipe = await unitOfWork.Recipes.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client", "Picture"])
            ?? throw new NotFoundException($"Recipe with this id is not found id = {id}");

        existRecipe.Update();

        existRecipe.ClientId = recipe.ClientId;
        existRecipe.StaffId = recipe.StaffId;
        existRecipe.Complaints = recipe.Complaints;
        existRecipe.Diagnosis = recipe.Diagnosis;
        existRecipe.CheckUps = recipe.CheckUps;
        existRecipe.Recommendations = recipe.Recommendations;
        //existRecipe.DateOfVisit = recipe.DateOfVisit;

        await unitOfWork.SaveAsync();

        return existRecipe;
    }
}