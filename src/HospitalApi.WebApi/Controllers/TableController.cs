using HospitalApi.Domain.Enums;
using HospitalApi.Service.Models;
using HospitalApi.WebApi.ApiServices.Tables;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Client), nameof(UserRole.Staff), nameof(UserRole.Owner))]

public class TableController(
    IAnalysisOfFecesTableApiService analysisOfFecesTableApiService,
    ITorchTableApiService torchApiService,
    IBiochemicalAnalysisOfBloodTableApiService biochemicalAnalysisOfBloodTableApiService,
    ICommonAnalysisOfBloodTableApiService commonAnalysisOfBloodTableApiService,
    ICommonAnalysisOfUrineTableApiService commonAnalysisOfUrineTableApiService) : ControllerBase
{
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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("analysis-of-feces-table/{id:long}")]
    public async ValueTask<IActionResult> PutAnalysisOfFecesTable(long id, AnalysisOfFecesTableUpdateDto updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await analysisOfFecesTableApiService.UpdateAsync(id, updateModel)
        });
    }

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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("biochemical-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutBiochemicalAnalysisOfBloodTable(long id, BiochemicalAnalysisOfBloodTableUpdateDto updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await biochemicalAnalysisOfBloodTableApiService.UpdateAsync(id, updateModel)
        });
    }

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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("common-analysis-of-blood-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfBloodTable(long id, CommonAnalysisOfBloodTableUpdateDto updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfBloodTableApiService.UpdateAsync(id, updateModel)
        });
    }

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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("common-analysis-of-urine-table/{id:long}")]
    public async ValueTask<IActionResult> PutCommonAnalysisOfUrineTable(long id, CommonAnalysisOfUrineTableUpdateDto updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await commonAnalysisOfUrineTableApiService.UpdateAsync(id, updateModel)
        });
    }

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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("torch/{id:long}")]
    public async ValueTask<IActionResult> PutTorchTable(long id, TorchTableUpdateDto updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await torchApiService.UpdateAsync(id, updateModel)
        });
    }
}