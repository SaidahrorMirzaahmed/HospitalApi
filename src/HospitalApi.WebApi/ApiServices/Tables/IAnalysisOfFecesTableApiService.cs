using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IAnalysisOfFecesTableApiService
{
    Task<AnalysisOfFecesTableViewModel> GetAsync(long id);

    Task<AnalysisOfFecesTableViewModel> UpdateAsync(long id, AnalysisOfFecesTableUpdateModel update);
}