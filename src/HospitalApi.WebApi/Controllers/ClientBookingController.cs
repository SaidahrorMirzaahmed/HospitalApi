using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.ClientBookings;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientBookingController(IClientBookingApiService service) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> Get(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.Get(id)
        });
    }

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
            Data = await service.GetByClientId(id, @params, filter, search)
        });
    }
}