﻿using AutoMapper;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.MedicalServiceTypeHistoryServices;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.MedicalServices;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.ClientBookings;

public class ClientBookingApiService(IMedicalTypeServiceHistoryService service, IMapper mapper) : IClientBookingApiService
{
    public async ValueTask<IEnumerable<MedicalServiceTypeHistoryViewModel>> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null)
    {
        var entities = await service.GetAllAsync(@params, filter, search);

        return mapper.Map<IEnumerable<MedicalServiceTypeHistoryViewModel>>(entities);
    }

    public async ValueTask<MedicalServiceTypeHistoryViewModel> GetAsync(long id)
    {
        var entity = await service.GetByIdAsync(id);

        return mapper.Map<MedicalServiceTypeHistoryViewModel>(entity);
    }

    public async ValueTask<IEnumerable<MedicalServiceTypeHistoryViewModel>> GetByClientIdAsync(long id, [FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null)
    {
        var entities = await service.GetByClientIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<MedicalServiceTypeHistoryViewModel>>(entities);
    }
}