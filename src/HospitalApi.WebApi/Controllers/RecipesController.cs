using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.ApiServices.Recipes;
using HospitalApi.WebApi.Models.Recipes;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class RecipesController(IRecipeApiService service) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(RecipeCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PostAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, RecipeUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PutAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });
    }
    
    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Client), nameof(UserRole.Owner))]
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAsync(id)
        });
    }
    
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllAsync(@params, filter, search)
        });
    }
}
