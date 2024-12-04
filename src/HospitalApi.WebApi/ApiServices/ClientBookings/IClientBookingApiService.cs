using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.MedicalServices;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.ClientBookings;

public interface IClientBookingApiService
{
    ValueTask<IEnumerable<MedicalServiceTypeHistoryViewModel>> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null);

    ValueTask<MedicalServiceTypeHistoryViewModel> GetAsync(long id);

    ValueTask<IEnumerable<MedicalServiceTypeHistoryViewModel>> GetByClientIdAsync(long id, [FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null);
}