using HospitalApi.WebApi.ApiServices.Accounts;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;

[AllowAnonymous]
[EnableCors("AllowSpecificOrigin")]
[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountApiService service) : ControllerBase
{
    [HttpGet("/send-code")]
    public async Task<IActionResult> SendSMSCodeAsync(string phone)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.SendSMSCodeAsync(phone)
        });
    }

    [HttpGet("/verify")]
    public async Task<IActionResult> Verify(string phone, long code)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.VerifySMSCode(phone, code)
        });
    }
}