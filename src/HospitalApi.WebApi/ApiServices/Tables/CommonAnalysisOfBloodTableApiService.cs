using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfBloodTableApiService(ICommonAnalysisOfBloodTableService service) : ICommonAnalysisOfBloodTableApiService
{
    public async Task<CommonAnalysisOfBloodTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(table);
    }

    public async Task<CommonAnalysisOfBloodTableDto> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateDto updateModel)
    {
        var updated = await service.UpdateAsync(id, CommonAnalysisOfBloodTableMapper.CreateCommonAnalysisOfBloodTable(id, updateModel));

        return CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(updated);
    }
}