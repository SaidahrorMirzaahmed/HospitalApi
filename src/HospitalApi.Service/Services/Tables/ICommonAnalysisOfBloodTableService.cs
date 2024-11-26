using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.Service.Services.Tables;

public interface ICommonAnalysisOfBloodTableService
{
    Task<CommonAnalysisOfBloodTable> GetAsync(long id);

    Task<CommonAnalysisOfBloodTable> CreateAsync(bool saveChanges = true);

    Task<CommonAnalysisOfBloodTable> UpdateAsync(long id, CommonAnalysisOfBloodTable table, bool saveChanges = true);

    Task<bool> DeleteAsync(long id, bool saveChanges = true);
}