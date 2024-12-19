using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfUrineTableApiService(ICommonAnalysisOfUrineTableService service) : ICommonAnalysisOfUrineTableApiService
{
    public async Task<CommonAnalysisOfUrineTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(table);
    }

    public async Task<CommonAnalysisOfUrineTableDto> UpdateAsync(long id, CommonAnalysisOfUrineTableUpdateDto update)
    {
        var table = CommonAnalysisOfUrineTableMapper.CreateCommonAnalysisOfUrineTable(id, update);
        var updated = await service.UpdateAsync(id, table);

        return CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(updated);
    }
}