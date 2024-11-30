using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.Tickets;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.Tickets;

public class TicketApiService(ITicketService service, IMapper mapper) : ITicketApiService
{
    public async Task<TicketViewModelModel> GetByIdAsync(long id)
    {
        var entity = await service.GetByIdAsync(id);

        return mapper.Map<TicketViewModelModel>(entity);
    }

    public async Task<IEnumerable<TicketViewModelModel>> GetByClientIdAsync(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        var entities = await service.GetByClientIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<TicketViewModelModel> CreateAsync(long clientId, long medicalServiceTypeId)
    {
        var entity = await service.CreateAsync(clientId, medicalServiceTypeId);

        return mapper.Map<TicketViewModelModel>(entity);
    }

    public async Task<TicketViewModelModel> CreateAsync(long clientId, IEnumerable<long> medicalServiceTypeIds)
    {
        var entity = await service.CreateAsync(clientId, medicalServiceTypeIds);

        return mapper.Map<TicketViewModelModel>(entity);
    }

    public async Task<TicketViewModelModel> UpdateAsync(bool isPaid, bool isCancelled)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        return await service.DeleteByIdAsync(id);
    }
}