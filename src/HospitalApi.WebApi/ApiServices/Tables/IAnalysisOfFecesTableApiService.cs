using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface IAnalysisOfFecesTableApiService
{
    Task<AnalysisOfFecesTableDto> GetAsync(long id);

    Task<LaboratoryViewModel> UpdateAsync(long id, AnalysisOfFecesTableUpdateDto update);
}