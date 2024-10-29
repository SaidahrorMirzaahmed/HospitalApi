using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Users;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Responses;
using HospitalApi.WebApi.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;

//[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class UsersController(IUserApiService service) : BaseController
{
    [HttpPost("/staff")]
    public async ValueTask<IActionResult> PostStaffAsync(UserCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PostStaffAsync(createModel)
        });
    }

    [AllowAnonymous]
    [HttpPost("/client")]
    public async ValueTask<IActionResult> PostNonAdminAsync(UserCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PostClientAsync(createModel)
        });
    }

    [AllowAnonymous]
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutClientAsync(long id, UserUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PutClientAsync(id, updateModel)
        });
    }

    [HttpPut("{id:long}/staff")]
    public async ValueTask<IActionResult> PutStaffAsync(long id, UserUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PutStaffAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync(
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

    [HttpGet("/client")]
    public async ValueTask<IActionResult> GetAllClient(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllClientAsync(@params, filter, search)
        });
    }

    [HttpGet("/staff")]
    public async ValueTask<IActionResult> GetAllStaff(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllStaffAsync(@params, filter, search)
        });
    }
}