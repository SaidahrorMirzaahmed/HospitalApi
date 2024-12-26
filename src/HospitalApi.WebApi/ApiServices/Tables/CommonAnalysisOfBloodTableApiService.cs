using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class CommonAnalysisOfBloodTableApiService(ICommonAnalysisOfBloodTableService service,
    IUnitOfWork unitOfWork,
    IPdfGeneratorService pdfGeneratorService,
    IMapper mapper) : ICommonAnalysisOfBloodTableApiService
{
    public async Task<CommonAnalysisOfBloodTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return CommonAnalysisOfBloodTableMapper.GetCommonAnalysisOfBloodTableView(table);
    }

    public async Task<LaboratoryViewModel> UpdateAsync(long id, CommonAnalysisOfBloodTableUpdateDto updateModel)
    {
        var updated = await service.UpdateAsync(id, CommonAnalysisOfBloodTableMapper.CreateCommonAnalysisOfBloodTable(id, updateModel));

        var lab = await unitOfWork.Laboratories.SelectAsync(entity => entity.TableId == id && entity.LaboratoryTableType == LaboratoryTableType.CommonAnalysisOfBlood && !entity.IsDeleted,
            includes: ["Staff", "Client", "PdfDetails"]);
        var document = await pdfGeneratorService.CreateDocument(lab);
        lab.PdfDetailsId = document.Id;
        lab.PdfDetails = document;
        await unitOfWork.Laboratories.UpdateAsync(lab);
        await unitOfWork.SaveAsync();

        return mapper.Map<LaboratoryViewModel>(lab);
    }
}