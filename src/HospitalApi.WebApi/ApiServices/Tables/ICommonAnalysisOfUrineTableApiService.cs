using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfUrineTableApiService
{
    Task<CommonAnalysisOfUrineTableViewModel> GetAsync(long id);

    Task<CommonAnalysisOfUrineTableViewModel> UpdateAsync(long id, CommonAnalysisOfUrineTableUpdateModel update);
}