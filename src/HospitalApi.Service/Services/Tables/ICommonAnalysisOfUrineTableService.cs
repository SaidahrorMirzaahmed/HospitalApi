using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.Service.Services.Tables;

public interface ICommonAnalysisOfUrineTableService
{
    Task<CommonAnalysisOfUrineTable> GetAsync(long id);

    Task<CommonAnalysisOfUrineTable> CreateAsync(bool saveChanges = true);

    Task<CommonAnalysisOfUrineTable> UpdateAsync(long id, CommonAnalysisOfUrineTable table, bool saveChanges = true);

    Task<bool> DeleteAsync(long id, bool saveChanges = true);
}