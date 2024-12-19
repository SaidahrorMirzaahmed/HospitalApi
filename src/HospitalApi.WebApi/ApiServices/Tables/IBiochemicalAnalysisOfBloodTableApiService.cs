using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IBiochemicalAnalysisOfBloodTableApiService
{
    Task<BiochemicalAnalysisOfBloodTableDto> GetAsync(long id);

    Task<BiochemicalAnalysisOfBloodTableDto> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateDto update);
}