using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class BiochemicalAnalysisOfBloodTableApiService(IBiochemicalAnalysisOfBloodTableService service) : IBiochemicalAnalysisOfBloodTableApiService
{
    public async Task<BiochemicalAnalysisOfBloodTableViewModel> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(table);
    }

    public async Task<BiochemicalAnalysisOfBloodTableViewModel> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateModel update)
    {
        var table = BiochemicalAnalysisOfBloodTableMapper.CreateBiochemicalAnalysisOfBloodTable(id, update);
        var updatedTable = await service.UpdateAsync(id, table);

        return BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(updatedTable);
    }
}