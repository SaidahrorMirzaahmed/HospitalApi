using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.ApiServices.Tables;
using HospitalApi.WebApi.Models.Responses;
using HospitalApi.WebApi.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class TableController(ITorchTableApiService apiService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("torch/{id:long}")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetAsync(id)
        });
    }

    [HttpPut("torch/{id:long}")]
    public async ValueTask<IActionResult> Put(long id, TorchTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.UpdateAsync(id, updateModel)
        });
    }
}