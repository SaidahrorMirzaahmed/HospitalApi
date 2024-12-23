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
        var serviceType = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted, includes: ["Staff", "ClinicQueue"])
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with the id = {id}");

        return serviceType;
    }

    public async Task<IEnumerable<MedicalServiceType>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var serviceTypes = unitOfWork.MedicalServiceTypes
            .SelectAsQueryable(type => !type.IsDeleted, includes: ["Staff", "ClinicQueue"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            serviceTypes = serviceTypes
                .Where(type => type.ServiceTypeTitle.ToLower().Contains(search.ToLower()) || type.ServiceTypeTitleRu.ToLower().Contains(search.ToLower())
                    || type.Staff.FirstName.ToLower().Contains(search.ToLower()) || type.Staff.LastName.ToLower().Contains(search.ToLower())
                    || type.Staff.Phone.Contains(search) || type.Staff.Address.ToLower().Contains(search.ToLower()));

        return await serviceTypes.ToListAsync();
    }

    public async Task<MedicalServiceType> CreateAsync(MedicalServiceType serviceType)
    {
        serviceType.Create();
        var type = await unitOfWork.MedicalServiceTypes.InsertAsync(serviceType);
        
        var queue = new ClinicQueue
        {
            MedicalServiceTypeId = type.Id,
            TodayQueue = 1,
            QueueDate = DateOnly.FromDateTime(DateTime.UtcNow),
            SecondDayQueue = 1,
            ThirdDayQueue = 1,
            FourthDayQueue = 1,
            FifthDayQueue = 1,
            SixthDayQueue = 1,
            SeventhDayQueue = 1,
        };
        queue.Create();
        var clinicQueue = await unitOfWork.ClinicQueues.InsertAsync(queue);
        type.ClinicQueue = clinicQueue;

        return type;
    }

    public async Task<MedicalServiceType> UpdateAsync(long id, MedicalServiceType serviceType)
    {
        var exists = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with id = {id}");

        exists.Update();
        exists.ServiceTypeTitle = serviceType.ServiceTypeTitle;
        exists.ServiceTypeTitleRu = serviceType.ServiceTypeTitleRu;
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