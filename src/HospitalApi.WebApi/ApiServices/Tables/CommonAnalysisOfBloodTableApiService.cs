using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Models.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfBloodTableApiService(ICommonAnalysisOfBloodTableService service) : ICommonAnalysisOfBloodTableApiService
{
    public async Task<CommonAnalysisOfBloodTableViewModel> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(table);
    }

    public async Task<CommonAnalysisOfBloodTableViewModel> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateModel updateModel)
    {
        var updated = await service.UpdateAsync(id, CommonAnalysisOfBloodTableMapper.CreateCommonAnalysisOfBloodTable(id, updateModel));

        return CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(updated);
    }
}