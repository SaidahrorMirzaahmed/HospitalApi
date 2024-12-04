using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Laboratories;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class LaboratoryController(ILaboratoryApiService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet("user/{id:long}")]
    public async ValueTask<IActionResult> GetByUserId(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllByUserIdAsync(id, @params, filter, search)
        });
    }

    [AllowAnonymous]
    [HttpGet]
    public async ValueTask<IActionResult> GetAll(
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

    [HttpPost("analysis-of-feces-table/client/{id:long}")]
    public async ValueTask<IActionResult> PostAnalysisOfFecesTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateByAnalysisOfFecesAsync(id)
        });
    }

    [HttpPost("biochemical-analysis-of-blood-table/client/{id:long}")]
    public async ValueTask<IActionResult> PostBiochemicalAnalysisOfBloodTable(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateByBiochemicalAnalysisOfBloodTableAsync(id)
        });
    }

    [HttpPost("common-analysis-of-blood-table/client/{id:long}")]
    public async ValueTask<IActionResult> PostCommonAnalysisOfBlood(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateByCommonAnalysisOfBloodAsync(id)
        });
    }

    [HttpPost("common-analysis-of-urine-table/client/{id:long}")]
    public async ValueTask<IActionResult> PostCommonAnalysisOfUrine(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateByCommonAnalysisOfUrineAsync(id)
        });
    }

    [HttpPost("torch/client/{id:long}")]
    public async ValueTask<IActionResult> PostTorch(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateByTorchAsync(id)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> Put(long id, LaboratoryUpdateModel update)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.UpdateAsync(id, update)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> Delete(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });
    }
}