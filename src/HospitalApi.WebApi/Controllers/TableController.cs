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
public class TableController(ITorchTableApiService torchApiService,
    IBiochemicalAnalysisOfBloodTableApiService biochemicalAnalysisOfBloodTableApiService,
    ICommonAnalysisOfBloodTableApiService commonAnalysisOfBloodService) : ControllerBase
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
    [HttpGet("biochemical-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> GetBiochemicalAnalysisOfBloodTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await biochemicalAnalysisOfBloodTableApiService.GetAsync(id)
        });
    }

    [HttpPut("biochemical-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutBiochemicalAnalysisOfBloodTable(long id, BiochemicalAnalysisOfBloodTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await biochemicalAnalysisOfBloodTableApiService.UpdateAsync(id, updateModel)
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
            Data = await commonAnalysisOfBloodService.GetAsync(id)
        });
    }

    [HttpPut("common-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfBloodTable(long id, CommonAnalysisOfBloodTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfBloodService.UpdateAsync(id, updateModel)
        });
    }
}