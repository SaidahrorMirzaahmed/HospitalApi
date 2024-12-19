using AutoMapper;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class TorchTableApiService(ITorchTableService service, IMapper mapper) : ITorchTableApiService
{
    public async Task<TorchTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return TorchMapper.GetTorchTableView(table);
    }

    public async Task<TorchTableDto> UpdateAsync(long id, TorchTableUpdateDto updateModel)
    {
        var updated = await service.UpdateAsync(id, TorchMapper.CreateTorchTable(id, updateModel));

        return TorchMapper.GetTorchTableView(updated);
    }
}