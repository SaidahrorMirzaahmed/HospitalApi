using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Service.Services.MedicalServices;

public class MedicalServiceTypeService(IUnitOfWork unitOfWork) : IMedicalServiceTypeService
{
    public async Task<MedicalServiceType> GetAsync(long id)
    {
        var serviceType = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted, includes: ["Staff"])
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with the id = {id}");

        return serviceType;
    }

    public async Task<IEnumerable<MedicalServiceType>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var serviceTypes = unitOfWork.MedicalServiceTypes
            .SelectAsQueryable(type => !type.IsDeleted, includes: ["Staff"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            serviceTypes = serviceTypes
                .Where(type => type.ServiceType.ToLower().Contains(search.ToLower()));

        return await serviceTypes.ToListAsync();
    }

    public async Task<MedicalServiceType> CreateAsync(MedicalServiceType serviceType)
    {
        serviceType.Create();
        serviceType.QueueDate = DateOnly.FromDateTime(DateTime.Now);
        var type = await unitOfWork.MedicalServiceTypes.InsertAsync(serviceType);

        return type;
    }

    public async Task<MedicalServiceType> UpdateAsync(long id, MedicalServiceType serviceType)
    {
        var exists = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with id = {id}");

        exists.Update();
        exists.ServiceType = serviceType.ServiceType;
        exists.Price = serviceType.Price;
        exists.StaffId = serviceType.StaffId;

        await unitOfWork.SaveAsync();

        return exists;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var exists = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with id = {id}");

        exists.Delete();
        await unitOfWork.MedicalServiceTypes.DeleteAsync(exists);
        await unitOfWork.SaveAsync();

        return true;
    }
}