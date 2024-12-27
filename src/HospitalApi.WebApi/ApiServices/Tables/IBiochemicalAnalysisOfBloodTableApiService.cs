using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IBiochemicalAnalysisOfBloodTableApiService
{
    Task<BiochemicalAnalysisOfBloodTableDto> GetAsync(long id);

    Task<LaboratoryViewModel> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateDto update);
}