using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IBiochemicalAnalysisOfBloodTableApiService
{
    Task<BiochemicalAnalysisOfBloodTableViewModel> GetAsync(long id);

    Task<BiochemicalAnalysisOfBloodTableViewModel> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateModel update);
}