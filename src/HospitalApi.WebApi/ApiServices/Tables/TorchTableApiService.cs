using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Mappers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.Tables;
using HospitalApi.WebApi.Models.Laboratories;

namespace HospitalApi.WebApi.ApiServices.Tables;

public class TorchTableApiService(ITorchTableService service, 
    IMapper mapper, 
    IUnitOfWork unitOfWork, 
    IPdfGeneratorService pdfGeneratorService) : ITorchTableApiService
{
    public async Task<TorchTableDto> GetAsync(long id)
    {
        var table = await service.GetAsync(id);

        return TorchMapper.GetTorchTableView(table);
    }

    public async Task<LaboratoryViewModel> UpdateAsync(long id, TorchTableUpdateDto updateModel)
    {
        var updated = await service.UpdateAsync(id, TorchMapper.CreateTorchTable(id, updateModel));

        var lab = await unitOfWork.Laboratories.SelectAsync(entity => entity.TableId == id &&  entity.LaboratoryTableType == LaboratoryTableType.Torch && !entity.IsDeleted, 
            includes: ["Staff", "Client", "PdfDetails"]);
        var document = await pdfGeneratorService.CreateDocument(lab);
        lab.PdfDetailsId = document.Id;
        lab.PdfDetails = document;
        await unitOfWork.Laboratories.UpdateAsync(lab);
        await unitOfWork.SaveAsync();

        return mapper.Map<LaboratoryViewModel>(lab);
    }
}