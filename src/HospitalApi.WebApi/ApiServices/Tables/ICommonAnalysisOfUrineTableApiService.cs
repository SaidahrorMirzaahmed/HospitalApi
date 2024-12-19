using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfUrineTableApiService
{
    Task<CommonAnalysisOfUrineTableDto> GetAsync(long id);

    Task<CommonAnalysisOfUrineTableDto> UpdateAsync(long id, CommonAnalysisOfUrineTableUpdateDto update);
}