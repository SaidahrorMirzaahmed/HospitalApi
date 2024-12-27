using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ICommonAnalysisOfBloodTableApiService
{
    Task<CommonAnalysisOfBloodTableDto> GetAsync(long id);

    Task<LaboratoryViewModel> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateDto update);
}