using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Diagnoses;

namespace HospitalApi.WebApi.ApiServices.DiagnosisApiServices;

public interface IDiagnosisApiService
{
    Task<IEnumerable<DiagnosisViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);

    Task<DiagnosisViewModel> GetByIdAsync(long id);
}