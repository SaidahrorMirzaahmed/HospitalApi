using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ITorchTableApiService
{
    Task<TorchTableDto> GetAsync(long id);

    Task<TorchTableDto> UpdateAsync(long id, TorchTableUpdateDto update);
}