﻿using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.Pdfs;

namespace HospitalApi.WebApi.ApiServices.Laboratories;

public interface ILaboratoryApiService
{
    Task<PdfDetailsViewModel> GeneratePdfAsync(long laboratoryId);

    Task<LaboratoryViewModel> CreateByTorchAsync(long clientId);

    Task<LaboratoryViewModel> CreateByAnalysisOfFecesAsync(long clientId);

    Task<LaboratoryViewModel> CreateByCommonAnalysisOfBloodAsync(long clientId);

    Task<LaboratoryViewModel> CreateByCommonAnalysisOfUrineAsync(long clientId);

    Task<LaboratoryViewModel> CreateByBiochemicalAnalysisOfBloodTableAsync(long clientId);

    Task<LaboratoryViewModel> UpdateAsync(long id, LaboratoryUpdateModel update);

    Task<bool> DeleteAsync(long id);

    Task<LaboratoryViewModel> GetAsync(long id);

    Task<IEnumerable<LaboratoryViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    Task<IEnumerable<LaboratoryViewModel>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}