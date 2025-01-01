using AutoMapper;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.Diagnoses;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Diagnoses;

namespace HospitalApi.WebApi.ApiServices.DiagnosisApiServices;

public class DiagnosisApiService(IDiagnosisService service, IMapper mapper) : IDiagnosisApiService
{
    public async Task<IEnumerable<DiagnosisViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var diagnoses = await service.GetAsync(@params, filter, search);

        return mapper.Map<IEnumerable<DiagnosisViewModel>>(diagnoses);
    }

    public async Task<DiagnosisViewModel> GetByIdAsync(long id)
    {
        var diagnosis = await service.GetByIdAsync(id);

        return mapper.Map<DiagnosisViewModel>(diagnosis);
    }
}