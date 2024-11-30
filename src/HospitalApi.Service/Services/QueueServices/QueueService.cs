using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;

namespace HospitalApi.Service.Services.QueueServices;

public class QueueService(IUnitOfWork unitOfWork) : IQueueService
{
    public async Task<MedicalServiceType> CreateQueueAsync(long id)
    {
        var entity = await unitOfWork.MedicalServiceTypes.SelectAsync(type => type.Id == id && !type.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists with id = {id}");
        
        if (entity.QueueDate != DateOnly.FromDateTime(DateTime.UtcNow))
        {
            entity.QueueDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.LastQueue = 1;
        }
        else
            entity.LastQueue++;

        return entity;
    }

    public async Task<IEnumerable<MedicalServiceType>> CreateQueuesAsync(IEnumerable<long> ids)
    {
        var entities = await unitOfWork.MedicalServiceTypes.SelectAsEnumerableAsync(type => ids.Contains(type.Id), includes: ["Staff"]);

        foreach (var entity in entities)
        {
            if (entity.QueueDate != DateOnly.FromDateTime(DateTime.UtcNow))
            {
                entity.QueueDate = DateOnly.FromDateTime(DateTime.UtcNow);
                entity.LastQueue = 0;
            }

            entity.LastQueue++;
        }

        return entities;
    }
}