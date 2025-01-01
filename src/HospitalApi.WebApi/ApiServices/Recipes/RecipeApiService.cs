using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.Recipes;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Recipes;
using HospitalApi.WebApi.Validations.Recipes;

namespace HospitalApi.WebApi.ApiServices.Recipes;

public class RecipeApiService(
    IMapper mapper,
    IRecipeService service,
    IUnitOfWork unitOfWork,
    IPdfGeneratorService pdfGeneratorService,
    RecipeCreateModelValidator validations,
    RecipeUpdateModelValidator validations1) : IRecipeApiService
{
    public async Task<PdfDetailsViewModel> PostPdfAsync(long id)
    {
        var entity = await service.GetAsync(id);
        await unitOfWork.BeginTransactionAsync();
        var pdf = await pdfGeneratorService.CreateDocument(entity);
        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<PdfDetailsViewModel>(pdf);
    }

    public async Task<RecipeViewModel> PostAsync(RecipeCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();
        var mapped = mapper.Map<Recipe>(createModel);
        mapped.StaffId = HttpContextHelper.UserId;
        var res = await service.CreateAsync(mapped, createModel.CheckUps);

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        var existsRecipe = await service.GetAsync(res.Id);
        var document = await pdfGeneratorService.CreateDocument(existsRecipe);
        existsRecipe.PdfDetailsId = document.Id;
        existsRecipe.PdfDetails = document;
        await unitOfWork.SaveAsync();

        return mapper.Map<RecipeViewModel>(existsRecipe);
    }

    public async Task<RecipeViewModel> PutAsync(long id, RecipeUpdateModel createModel)
    {
        await validations1.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();

        var mappedRecipe = mapper.Map<Recipe>(createModel);
        mappedRecipe.StaffId = HttpContextHelper.UserId;
        var updatedRecipe = await service.UpdateAsync(id, mappedRecipe, createModel.CheckUps);

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        var existsRecipe = await service.GetAsync(updatedRecipe.Id);
        var document = await pdfGeneratorService.CreateDocument(existsRecipe);
        existsRecipe.PdfDetailsId = document.Id;
        existsRecipe.PdfDetails = document;
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
        IEnumerable<Recipe> newsList = await service.GetAllAsync(@params, filter, search);
        var res = newsList.AsEnumerable();
        return mapper.Map<IEnumerable<RecipeViewModel>>(res);
    }

    public async Task<IEnumerable<RecipeViewModel>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var newsList = await service.GetAllByUserIdAsync(id, @params, filter, search);
        var res = newsList.AsEnumerable();
        return mapper.Map<IEnumerable<RecipeViewModel>>(res);
    }

    public async Task<RecipeViewModel> GetAsync(long id)
    {
        return mapper.Map<RecipeViewModel>(await service.GetAsync(id));
    }
}