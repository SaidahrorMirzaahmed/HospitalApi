using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfUrineTableApiService(ICommonAnalysisOfUrineTableService service) : ICommonAnalysisOfUrineTableApiService
{
    public async Task<CommonAnalysisOfUrineTableViewModel> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(table);
    }

    public async Task<CommonAnalysisOfUrineTableViewModel> UpdateAsync(long id, CommonAnalysisOfUrineTableUpdateModel update)
    {
        var table = CommonAnalysisOfUrineTableMapper.CreateCommonAnalysisOfUrineTable(id, update);
        var updated = await service.UpdateAsync(id, table);

        return CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(updated);
    }
}