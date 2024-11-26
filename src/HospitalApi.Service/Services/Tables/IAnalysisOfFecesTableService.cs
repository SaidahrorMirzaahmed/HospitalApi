using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.Service.Services.Tables;

public interface IAnalysisOfFecesTableService
{
    Task<AnalysisOfFecesTable> GetAsync(long id);

    Task<AnalysisOfFecesTable> CreateAsync(bool saveChanges = true);

    Task<AnalysisOfFecesTable> UpdateAsync(long id, AnalysisOfFecesTable table, bool saveChanges = true);

    Task<bool> DeleteAsync(long id, bool saveChanges = true);
}