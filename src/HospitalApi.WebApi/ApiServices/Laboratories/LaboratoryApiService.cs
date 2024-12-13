using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.Laboratories;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.Pdfs;

namespace HospitalApi.WebApi.ApiServices.Laboratories;

public class LaboratoryApiService(ILaboratoryService laboratoryService, IPdfGeneratorService pdfGeneratorService, IMapper mapper) : ILaboratoryApiService
{
    public async Task<PdfDetailsViewModel> GeneratePdfAsync(long laboratoryId)
    {
        var laboratory = await laboratoryService.GetAsync(laboratoryId);
        var entity = await pdfGeneratorService.CreateDocument(laboratory);

        return mapper.Map<PdfDetailsViewModel>(entity);
    }

    public async Task<LaboratoryViewModel> CreateByTorchAsync(long clientId)
    {
        var laboratory = await laboratoryService.CreateByTorchTableAsync(clientId);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<LaboratoryViewModel> CreateByAnalysisOfFecesAsync(long clientId)
    {
        var laboratory = await laboratoryService.CreateByAnalysisOfFecesTableAsync(clientId);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<LaboratoryViewModel> CreateByCommonAnalysisOfBloodAsync(long clientId)
    {
        var laboratory = await laboratoryService.CreateByCommonAnalysisOfBloodAsync(clientId);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<LaboratoryViewModel> CreateByCommonAnalysisOfUrineAsync(long clientId)
    {
        var laboratory = await laboratoryService.CreateByCommonAnalysisOfUrineAsync(clientId);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<LaboratoryViewModel> CreateByBiochemicalAnalysisOfBloodTableAsync(long clientId)
    {
        var laboratory = await laboratoryService.CreateByBiochemicalAnalysisOfBloodTableAsync(clientId);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<LaboratoryViewModel> UpdateAsync(long id, LaboratoryUpdateModel update)
    {
        var laboratory = await laboratoryService.UpdateAsync(id, update.ClientId, update.LaboratoryTableType);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await laboratoryService.DeleteAsync(id);
    }

    public async Task<IEnumerable<LaboratoryViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var laboratories = await laboratoryService.GetAllAsync(@params, filter, search);

        return mapper.Map<IEnumerable<LaboratoryViewModel>>(laboratories);
    }

    public async Task<IEnumerable<LaboratoryViewModel>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var laboratories = await laboratoryService.GetAllByUserIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<LaboratoryViewModel>>(laboratories);
    }

    public async Task<LaboratoryViewModel> GetAsync(long id)
    {
        var laboratory = await laboratoryService.GetAsync(id);

        return mapper.Map<LaboratoryViewModel>(laboratory);
    }
}