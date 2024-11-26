using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.MedicalServices;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.MedicalServices;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Owner))]
public class MedicalServiceTypeController(IMedicalServiceTypeApiService apiService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet]
    public async ValueTask<IActionResult> GetAll(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string searchQuery = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetAllAsync(@params, filter, searchQuery)
        });
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post(MedicalServiceTypeCreateModel create)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.CreateAsync(create)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> Put(long id, MedicalServiceTypeUpdateModel update)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.UpdateAsync(id, update)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> Delete(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.DeleteAsync(id)
        });
    }
}