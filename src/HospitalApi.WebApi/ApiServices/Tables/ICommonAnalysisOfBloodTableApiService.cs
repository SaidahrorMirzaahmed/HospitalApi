using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfBloodTableApiService
{
    Task<CommonAnalysisOfBloodTableDto> GetAsync(long id);

    Task<CommonAnalysisOfBloodTableDto> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateDto update);
}