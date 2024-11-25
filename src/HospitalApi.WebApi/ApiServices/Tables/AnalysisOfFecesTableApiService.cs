using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Models.Tables;
namespace HospitalApi.WebApi.ApiServices.Tables;

public class AnalysisOfFecesTableApiService(IAnalysisOfFecesTableService service) : IAnalysisOfFecesTableApiService
{
    public async Task<AnalysisOfFecesTableViewModel> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(table);
    }

    public async Task<AnalysisOfFecesTableViewModel> UpdateAsync(long id, AnalysisOfFecesTableUpdateModel update)
    {
        var table = AnalysisOfFecesTableMapper.CreateAnalysisOfFecesTable(id, update);
        var updated = await service.UpdateAsync(id, table);

        return AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(updated);
    }
}