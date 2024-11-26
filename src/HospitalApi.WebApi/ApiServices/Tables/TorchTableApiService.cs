using AutoMapper;
using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class TorchTableApiService(ITorchTableService service, IMapper mapper) : ITorchTableApiService
{
    public async Task<TorchTableViewModel> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return TorchMapper.GetTorchTableView(table);
    }

    public async Task<TorchTableViewModel> UpdateAsync(long id, TorchTableUpdateModel updateModel)
    {
        var updated = await service.UpdateAsync(id, TorchMapper.CreateTorchTable(id, updateModel));

        return TorchMapper.GetTorchTableView(updated);
    }
}