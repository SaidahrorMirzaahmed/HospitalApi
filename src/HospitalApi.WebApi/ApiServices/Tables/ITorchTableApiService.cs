using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ITorchTableApiService
{
    Task<TorchTableViewModel> GetAsync(long id);

    Task<TorchTableViewModel> UpdateAsync(long id, TorchTableUpdateModel update);
}