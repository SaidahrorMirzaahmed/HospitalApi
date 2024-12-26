using HospitalApi.Service.Services.Tables;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Service.Services.PdfGeneratorServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HospitalApi.Domain.Enums;
using AutoMapper;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class BiochemicalAnalysisOfBloodTableApiService(IBiochemicalAnalysisOfBloodTableService service,
    IUnitOfWork unitOfWork,
    IPdfGeneratorService pdfGeneratorService,
    IMapper mapper) : IBiochemicalAnalysisOfBloodTableApiService
{
    public async Task<BiochemicalAnalysisOfBloodTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return BiochemicalAnalysisOfBloodTableMapper.GetBiochemicalAnalysisOfBloodTableView(table);
    }

    public async Task<LaboratoryViewModel> UpdateAsync(long id, BiochemicalAnalysisOfBloodTableUpdateDto update)
    {
        var table = BiochemicalAnalysisOfBloodTableMapper.CreateBiochemicalAnalysisOfBloodTable(id, update);
        var updatedTable = await service.UpdateAsync(id, table);

        var lab = await unitOfWork.Laboratories.SelectAsync(entity => entity.TableId == id && entity.LaboratoryTableType == LaboratoryTableType.BiochemicalAnalysisOfBlood && !entity.IsDeleted, 
            includes: ["Staff", "Client", "PdfDetails"]);
        var document = await pdfGeneratorService.CreateDocument(lab);
        lab.PdfDetailsId = document.Id;
        lab.PdfDetails = document;
        await unitOfWork.SaveAsync();

        return mapper.Map<LaboratoryViewModel>(lab);
    }
}