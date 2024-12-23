﻿using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Tickets;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Responses;
using HospitalApi.WebApi.Models.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[CustomAuthorize(nameof(UserRole.Client), nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class TicketController(ITicketApiService apiService) : ControllerBase
{
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
            Data = await apiService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpPost("pdf/{id:long}")]
    public async ValueTask<IActionResult> GetPdf(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.GetPdf(id)
        });
    }

    [HttpPost("client-id/{clientId:long}")]
    public async ValueTask<IActionResult> PostByIds(long clientId, IEnumerable<TicketCreateModel> ticketCreateModels)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.CreateAsync(clientId, ticketCreateModels)
        });
    }

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> Put(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await apiService.UpdateAsync(id)
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