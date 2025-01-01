using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.DiagnosisApiServices;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class DiagnosisController(IDiagnosisApiService service) : BaseController
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAsync(@params, filter, search)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetByIdAsync(id)
        });
    }
}