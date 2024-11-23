﻿using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Laboratories;
using HospitalApi.WebApi.Configurations;
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
    public async ValueTask<IActionResult> Put(long id, long clientId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.UpdateAsync(id, clientId)
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