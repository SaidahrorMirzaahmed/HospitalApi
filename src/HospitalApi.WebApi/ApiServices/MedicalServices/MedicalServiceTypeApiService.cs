using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.MedicalServices;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.MedicalServices;
using HospitalApi.WebApi.Validations.MedicalServices;

namespace HospitalApi.WebApi.ApiServices.MedicalServices;

public class MedicalServiceTypeApiService(IUnitOfWork unitOfWork,
    IMedicalServiceTypeService service,
    IMapper mapper,
    MedicalServiceTypeCreateModelValidator createModelValidator,
    MedicalServiceTypeUpdateModelValidator updateModelValidator) : IMedicalServiceTypeApiService
{
    public async Task<MedicalServiceTypeViewModel> GetAsync(long id)
    {
        var serviceType = await service.GetAsync(id);

        return mapper.Map<MedicalServiceTypeViewModel>(serviceType);
    }

    public async Task<IEnumerable<MedicalServiceTypeViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var serviceTypes = await service.GetAllAsync(@params, filter, search);

        return mapper.Map<IEnumerable<MedicalServiceTypeViewModel>>(serviceTypes);
    }

    public async Task<MedicalServiceTypeViewModel> CreateAsync(MedicalServiceTypeCreateModel serviceType)
    {
        await createModelValidator.EnsureValidatedAsync(serviceType);
        var type = await service.CreateAsync(mapper.Map<MedicalServiceType>(serviceType));
        await unitOfWork.SaveAsync();

        return mapper.Map<MedicalServiceTypeViewModel>(type);
    }

    public async Task<MedicalServiceTypeViewModel> UpdateAsync(long id, MedicalServiceTypeUpdateModel serviceType)
    {
        await updateModelValidator.EnsureValidatedAsync(serviceType);
        var type = await service.UpdateAsync(id, mapper.Map<MedicalServiceType>(serviceType));

        return mapper.Map<MedicalServiceTypeViewModel>(type);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var isDeleted = await service.DeleteAsync(id);

        return await Task.FromResult(isDeleted);
    }
}