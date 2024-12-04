using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.MedicalServiceTypeHistoryServices;

public interface IMedicalTypeServiceHistoryService
{
    Task<IEnumerable<MedicalServiceTypeHistory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    Task<MedicalServiceTypeHistory> GetByIdAsync(long id);

    Task<IEnumerable<MedicalServiceTypeHistory>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null);
    
    Task<IEnumerable<MedicalServiceTypeHistory>> GetByTicketIdAsync(long ticketId, PaginationParams @params, Filter filter, string search = null);
}