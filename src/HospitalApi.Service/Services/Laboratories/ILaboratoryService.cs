using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Laboratories;

public interface ILaboratoryService
{
    Task<Laboratory> CreateByTorchTableAsync(long clientId);

    Task<Laboratory> UpdateAsync(long id, long clientId);
    
    Task<bool> DeleteAsync(long id);
    
    Task<Laboratory> GetAsync(long id);
    
    Task<IEnumerable<Laboratory>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    
    Task<IEnumerable<Laboratory>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}