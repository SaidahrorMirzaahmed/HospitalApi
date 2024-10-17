using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;


[EnableCors("AllowSpecificOrigin")]
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
}
