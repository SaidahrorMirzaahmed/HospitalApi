using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.ApiServices.StatisticsDetails;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class StatisticsController(IStatisticsApiService apiService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetLastYearAllStatisticsAsync(),
        });
    }
}