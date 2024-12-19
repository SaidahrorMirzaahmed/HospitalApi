using HospitalApi.Service.Services.Tables;
using HospitalApi.Service.Models;
using HospitalApi.Service.Mappers;
namespace HospitalApi.WebApi.ApiServices.Tables;

public class AnalysisOfFecesTableApiService(IAnalysisOfFecesTableService service) : IAnalysisOfFecesTableApiService
{
    public async Task<AnalysisOfFecesTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(table);
    }

    public async Task<AnalysisOfFecesTableDto> UpdateAsync(long id, AnalysisOfFecesTableUpdateDto update)
    {
        var table = AnalysisOfFecesTableMapper.CreateAnalysisOfFecesTable(id, update);
        var updated = await service.UpdateAsync(id, table);

        return AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(updated);
    }
}