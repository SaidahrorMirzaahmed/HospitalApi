using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Service.Services.MedicalServiceTypeHistoryServices;

public class MedicalTypeServiceHistoryService(IUnitOfWork unitOfWork) : IMedicalTypeServiceHistoryService
{
    public async Task<IEnumerable<MedicalServiceTypeHistory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.MedicalServiceTypeHistories
            .SelectAsQueryable(history => !history.IsDeleted)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities
                .Where(entity => entity.Client.FirstName.ToLower().Contains(search.ToLower())
                    || entity.Client.LastName.ToLower().Contains(search.ToLower())
                    || entity.MedicalServiceType.ServiceType.ToLower().Contains(search.ToLower()));

        return await entities.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async Task<MedicalServiceTypeHistory> GetByIdAsync(long id)
    {
        var entity = await unitOfWork.MedicalServiceTypeHistories.SelectAsync(history => history.Id == id && !history.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceTypeHistory)} is not exists with id = {id}");

        return entity;
    }

    public async Task<IEnumerable<MedicalServiceTypeHistory>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.MedicalServiceTypeHistories
            .SelectAsQueryable(history => history.ClientId == clientId && !history.IsDeleted)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities.Where(entity => entity.MedicalServiceType.ServiceType.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }

    public async Task<IEnumerable<MedicalServiceTypeHistory>> GetByTicketIdAsync(long ticketId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.MedicalServiceTypeHistories
            .SelectAsQueryable(history => history.TicketId == ticketId && !history.IsDeleted)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities.Where(entity => entity.MedicalServiceType.ServiceType.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }
}