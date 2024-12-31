using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Diagnoses;

public interface IDiagnosisService
{
    Task<IEnumerable<Diagnosis>> GetAsync(PaginationParams @params, Filter filter, string search = null);

    Task<Diagnosis> GetByIdAsync(long id);
}