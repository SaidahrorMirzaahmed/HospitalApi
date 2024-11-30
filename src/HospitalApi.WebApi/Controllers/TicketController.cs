using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Tickets;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Client), nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class TicketController(ITicketApiService apiService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetByIdAsync(id)
        });
    }

    [AllowAnonymous]
    [HttpGet("client/{id:long}")]
    public async ValueTask<IActionResult> GetByClientId(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetByClientIdAsync(id, @params, filter, search)
        });
    }

    [HttpPost("client-id/{clientId:long}/{medicalServiceTypeId:long}")]
    public async ValueTask<IActionResult> Post(long clientId, long medicalServiceTypeId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.CreateAsync(clientId, medicalServiceTypeId)
        });
    }

    [HttpPost("client-id/{clientId:long}")]
    public async ValueTask<IActionResult> PostByIds(long clientId, IEnumerable<long> medicalServiceTypeId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.CreateAsync(clientId, medicalServiceTypeId)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteById(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.DeleteByIdAsync(id)
        });
    }
}