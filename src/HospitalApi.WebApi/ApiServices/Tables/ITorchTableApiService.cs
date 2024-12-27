using HospitalApi.Service.Models;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public interface ITorchTableApiService
{
    Task<TorchTableDto> GetAsync(long id);

    Task<LaboratoryViewModel> UpdateAsync(long id, TorchTableUpdateDto update);
}