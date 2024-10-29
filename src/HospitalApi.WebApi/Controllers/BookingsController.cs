using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.ApiServices.Bookings;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.Controllers;

[CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Owner))]
public class BookingsController(IBookingApiService service) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(BookingCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PostAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, BookingUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.PutAsync(id, updateModel)
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

    [CustomAuthorize(nameof(UserRole.Staff), nameof(UserRole.Client), nameof(UserRole.Owner))]
    [AllowAnonymous]
    [HttpGet("user-id/{id:long}")]
    public async ValueTask<IActionResult> GetbyUserIdAsync(
        long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllbyUserIdAsync(id, @params, filter, search)
        });
    }

    [AllowAnonymous]
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

    //[HttpPost("{id:long}/files/upload")]
    //public async Task<IActionResult> PictureUploadAsync(long id, AssetCreateModel asset)
    //{
    //    return Ok(new Response
    //    {
    //        StatusCode = 200,
    //        Message = "Ok",
    //        Data = await service.UploadPictureAsync(id, asset)
    //    });
    //}

    //[HttpDelete("{id:long}/files/delete")]
    //public async Task<IActionResult> PictureDeleteAsync(long id)
    //{
    //    return Ok(new Response
    //    {
    //        StatusCode = 200,
    //        Message = "Success",
    //        Data = await service.DeletePictureAsync(id)
    //    });
    //}
}