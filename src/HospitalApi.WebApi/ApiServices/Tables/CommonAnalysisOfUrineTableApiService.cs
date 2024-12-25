using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.Tables;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfUrineTableApiService(ICommonAnalysisOfUrineTableService service, IUnitOfWork unitOfWork, IPdfGeneratorService pdfGeneratorService) : ICommonAnalysisOfUrineTableApiService
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
        
        var lab = await unitOfWork.Laboratories.SelectAsync(entity => entity.TableId == id && entity.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfUrine && !entity.IsDeleted,
            includes: ["Staff", "Client", "PdfDetails"]);
        var document = await pdfGeneratorService.CreateDocument(lab);
        lab.PdfDetailsId = document.Id;
        lab.PdfDetails = document;
        await unitOfWork.Laboratories.UpdateAsync(lab);
        await unitOfWork.SaveAsync();

        return CommonAnalysisOfUrineTableMapper.GetCommonAnalysisOfUrineTableView(updated);
    }
}