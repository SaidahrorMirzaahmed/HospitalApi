using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.MedicalServices;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.ClientBookings;

public interface IClientBookingApiService
{
    ValueTask<MedicalServiceTypeHistoryViewModel> Get(long id);

    ValueTask<IEnumerable<MedicalServiceTypeHistoryViewModel>> GetByClientId(long id, [FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null);
}