using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.Service.Services.Tables;

public interface IBiochemicalAnalysisOfBloodTableService
{
    Task<BiochemicalAnalysisOfBloodTable> GetAsync(long id);

    Task<BiochemicalAnalysisOfBloodTable> CreateAsync(bool saveChanges = true);

    Task<BiochemicalAnalysisOfBloodTable> UpdateAsync(long id, BiochemicalAnalysisOfBloodTable table, bool saveChanges = true);

    Task<bool> DeleteAsync(long id, bool saveChanges = true);
}