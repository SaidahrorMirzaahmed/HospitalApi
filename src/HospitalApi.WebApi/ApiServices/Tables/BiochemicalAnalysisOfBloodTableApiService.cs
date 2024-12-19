using HospitalApi.Service.Services.Tables;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class BiochemicalAnalysisOfBloodTableApiService(IBiochemicalAnalysisOfBloodTableService service) : IBiochemicalAnalysisOfBloodTableApiService
{
    public async Task<BiochemicalAnalysisOfBloodTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(table);
    }

    public async Task<BiochemicalAnalysisOfBloodTableDto> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateDto update)
    {
        var table = BiochemicalAnalysisOfBloodTableMapper.CreateBiochemicalAnalysisOfBloodTable(id, update);
        var updatedTable = await service.UpdateAsync(id, table);

        return BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(updatedTable);
    }
}