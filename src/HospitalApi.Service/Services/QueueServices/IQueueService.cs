using HospitalApi.Domain.Entities;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Services.QueueServices;

public interface IQueueService
{
    Task<IEnumerable<(MedicalServiceType MedicalServiceType, DateOnly BookingDate)>> CreateQueuesAsync(IEnumerable<TicketCreateDto> dtos);
}