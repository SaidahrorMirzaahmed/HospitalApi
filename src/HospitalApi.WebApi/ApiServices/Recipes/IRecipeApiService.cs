using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Recipes;

namespace HospitalApi.WebApi.ApiServices.Recipes;

public interface IRecipeApiService
{
    Task<PdfDetailsViewModel> PostPdfAsync(long id);
    Task<RecipeViewModel> PostAsync(RecipeCreateModel createModel);
    Task<RecipeViewModel> PutAsync(long id, RecipeUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<RecipeViewModel> GetAsync(long id);
    Task<IEnumerable<RecipeViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<RecipeViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}