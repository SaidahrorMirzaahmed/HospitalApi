using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.Service.Services.Tables;

public interface ITorchTableService
{
    Task<TorchTable> GetAsync(long id);

    Task<TorchTable> CreateAsync(bool saveChanges = true);

    Task<TorchTable> UpdateAsync(long id, TorchTable table, bool saveChanges = true);

    Task<bool> DeleteAsync(long tableId, bool saveChanges = true);
}