using HospitalApi.Domain.Entities;

namespace HospitalApi.Service.Services.QueueServices;

public interface IQueueService
{
    Task<MedicalServiceType> CreateQueueAsync(long id);

    Task<IEnumerable<MedicalServiceType>> CreateQueuesAsync(IEnumerable<long> ids);
}