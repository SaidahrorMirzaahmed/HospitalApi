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
public class TableController(
    IAnalysisOfFecesTableApiService analysisOfFecesTableApiService,
    ITorchTableApiService torchApiService,
    IBiochemicalAnalysisOfBloodTableApiService biochemicalAnalysisOfBloodTableApiService,
    ICommonAnalysisOfBloodTableApiService commonAnalysisOfBloodTableApiService,
    ICommonAnalysisOfUrineTableApiService commonAnalysisOfUrineTableApiService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("analysis-of-feces-table/{id:long}")]
    public async ValueTask<IActionResult> GetAnalysisOfFecesTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await analysisOfFecesTableApiService.GetAsync(id)
        });
    }

    [HttpPut("analysis-of-feces-table/{id:long}")]
    public async ValueTask<IActionResult> PutAnalysisOfFecesTable(long id, AnalysisOfFecesTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await analysisOfFecesTableApiService.UpdateAsync(id, updateModel)
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
            Data = await commonAnalysisOfBloodTableApiService.GetAsync(id)
        });
    }

    [HttpPut("common-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfBloodTable(long id, CommonAnalysisOfBloodTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfBloodTableApiService.UpdateAsync(id, updateModel)
        });
    }

    [AllowAnonymous]
    [HttpGet("common-analysis-of-urine-table/{id:long}")]
    public async ValueTask<IActionResult> GetCommonAnalysisOfUrineTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfUrineTableApiService.GetAsync(id)
        });
    }

    [HttpPut("common-analysis-of-urine-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfUrineTable(long id, CommonAnalysisOfUrineTableUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfUrineTableApiService.UpdateAsync(id, updateModel)
        });
    }

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
}