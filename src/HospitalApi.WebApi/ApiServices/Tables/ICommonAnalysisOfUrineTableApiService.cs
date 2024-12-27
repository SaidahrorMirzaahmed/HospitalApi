using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfUrineTableApiService
{
    Task<CommonAnalysisOfUrineTableDto> GetAsync(long id);

    Task<LaboratoryViewModel> UpdateAsync(long id, CommonAnalysisOfUrineTableUpdateDto update);
}