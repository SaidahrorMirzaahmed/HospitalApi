using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.MedicalServices;

public interface IMedicalServiceTypeService
{
    Task<MedicalServiceType> GetAsync(long id);
    Task<IEnumerable<MedicalServiceType>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<MedicalServiceType> CreateAsync(MedicalServiceType serviceType);
    Task<MedicalServiceType> UpdateAsync(long id, MedicalServiceType serviceType);
    Task<bool> DeleteAsync(long id);
}