using HospitalApi.Service.Services.Tables;
using HospitalApi.Service.Models;
using HospitalApi.Service.Mappers;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Domain.Enums;
namespace HospitalApi.WebApi.ApiServices.Tables;

public class AnalysisOfFecesTableApiService(IAnalysisOfFecesTableService service, IUnitOfWork unitOfWork, IPdfGeneratorService pdfGeneratorService) : IAnalysisOfFecesTableApiService
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
        
        var lab = await unitOfWork.Laboratories.SelectAsync(entity => entity.TableId == id && entity.LaboratoryTableType == LaboratoryTableType.AnalysisOfFeces && !entity.IsDeleted,
            includes: ["Staff", "Client", "PdfDetails"]);
        var document = await pdfGeneratorService.CreateDocument(lab);
        lab.PdfDetailsId = document.Id;
        lab.PdfDetails = document;
        await unitOfWork.SaveAsync();

        return AnalysisOfFecesTableMapper.GetAnalysisOfFecesTableView(updated);
    }
}