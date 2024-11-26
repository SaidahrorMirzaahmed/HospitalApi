using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfBloodTableApiService
{
    Task<CommonAnalysisOfBloodTableViewModel> GetAsync(long id);

    Task<CommonAnalysisOfBloodTableViewModel> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateModel update);
}