using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.MedicalServices;

namespace HospitalApi.WebApi.ApiServices.MedicalServices;

public interface IMedicalServiceTypeApiService
{
    Task<MedicalServiceTypeViewModel> GetAsync(long id);
    Task<IEnumerable<MedicalServiceTypeViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<MedicalServiceTypeViewModel> CreateAsync(MedicalServiceTypeCreateModel serviceType);
    Task<MedicalServiceTypeViewModel> UpdateAsync(long id, MedicalServiceTypeUpdateModel serviceType);
    Task<bool> DeleteAsync(long id);
}