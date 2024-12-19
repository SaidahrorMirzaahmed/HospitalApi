using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IAnalysisOfFecesTableApiService
{
    Task<AnalysisOfFecesTableDto> GetAsync(long id);

    Task<AnalysisOfFecesTableDto> UpdateAsync(long id, AnalysisOfFecesTableUpdateDto update);
}