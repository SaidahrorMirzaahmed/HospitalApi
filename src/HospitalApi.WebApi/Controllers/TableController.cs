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
public class TableController(ITorchTableApiService torchApiService, ICommonAnalysisOfBloodTableApiService commonAnalysisOfBlood) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("torch/{id:long}")]
    public async ValueTask<IActionResult> GetTorchTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await torchApiService.GetAsync(id)
        });
    }

    [HttpPut("torch/{id:long}")]
    public async ValueTask<IActionResult> PutTorchTable(long id, TorchTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await torchApiService.UpdateAsync(id, updateModel)
        });
    }

    [AllowAnonymous]
    [HttpGet("common-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> GetCommonAnalysisOfBloodTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfBlood.GetAsync(id)
        });
    }

    [HttpPut("common-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfBloodTable(long id, CommonAnalysisOfBloodTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfBlood.UpdateAsync(id, updateModel)
        });
    }
}