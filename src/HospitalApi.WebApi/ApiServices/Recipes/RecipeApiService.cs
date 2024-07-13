using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Services.Assets;
using HospitalApi.Service.Services.Recipes;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.Recipes;
using HospitalApi.WebApi.Validations.Recipes;
using Tenge.Service.Configurations;
using Tenge.Service.Helpers;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Recipes;

public class RecipeApiService(
    IMapper mapper,
    IRecipeService service,
    IUnitOfWork unitOfWork,
    IAssetService assetService,
    RecipeCreateModelValidator validations,
    RecipeUpdateModelValidator validations1) : IRecipeApiService
{
    public async Task<RecipeViewModel> PostAsync(RecipeCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();
        var mapped = mapper.Map<Recipe>(createModel);
        mapped.StaffId = HttpContextHelper.UserId;

        var res = await service.CreateAsync(mapped);
        var asset = await assetService.UploadAsync(createModel.Picture);

        res.PictureId = asset.Id;
        res.Picture = asset;

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<RecipeViewModel>(res);
    }

    public async Task<RecipeViewModel> PutAsync(long id, RecipeUpdateModel createModel)
    {
        await validations1.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();

        var mappedRecipe = mapper.Map<Recipe>(createModel);
        mappedRecipe.StaffId = HttpContextHelper.UserId;
        var updatedRecipe = await service.UpdateAsync(id, mappedRecipe);

        var asset = await assetService.UploadAsync(createModel.Picture);

        updatedRecipe.PictureId = asset.Id;
        updatedRecipe.Picture = asset;

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<RecipeViewModel>(updatedRecipe);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await service.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<RecipeViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var newsList = await service.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<RecipeViewModel>>(newsList);
    }

    public async Task<IEnumerable<RecipeViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var newsList = await service.GetAllByUserIdAsync(id, @params, filter, search);
        return mapper.Map<IEnumerable<RecipeViewModel>>(newsList);
    }

    public async Task<RecipeViewModel> GetAsync(long id)
    {
        return mapper.Map<RecipeViewModel>(await service.GetAsync(id));
    }
}
