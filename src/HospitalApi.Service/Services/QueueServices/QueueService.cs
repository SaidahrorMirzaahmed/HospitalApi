using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Services.QueueServices;

public class QueueService(IUnitOfWork unitOfWork) : IQueueService
{
    public async Task<IEnumerable<(MedicalServiceType MedicalServiceType, DateOnly BookingDate)>> CreateQueuesAsync(IEnumerable<TicketCreateDto> dtos)
    {
        
        var dtoLookup = dtos.ToDictionary(dto => dto.MedicalServiceId);
        var entities = (await unitOfWork.MedicalServiceTypes
            .SelectAsEnumerableAsync(type => !type.IsDeleted && dtoLookup.Keys.ToList().Contains(type.Id), includes: ["Staff"]))
            .Select(entity => (
                MedicalServiceType: entity,
                BookingDate: dtoLookup[entity.Id].BookingDate
            ));

        foreach (var type in entities)
        {
            if (type.MedicalServiceType.ClinicQueue.QueueDate != DateOnly.FromDateTime(DateTime.UtcNow))
            {
                type.MedicalServiceType.ClinicQueue.QueueDate = DateOnly.FromDateTime(DateTime.UtcNow);
                type.MedicalServiceType.ClinicQueue.TodayQueue = type.MedicalServiceType.ClinicQueue.SeventhDayQueue;
                type.MedicalServiceType.ClinicQueue.SecondDayQueue = type.MedicalServiceType.ClinicQueue.ThirdDayQueue;
                type.MedicalServiceType.ClinicQueue.ThirdDayQueue = type.MedicalServiceType.ClinicQueue.FourthDayQueue;
                type.MedicalServiceType.ClinicQueue.FourthDayQueue = type.MedicalServiceType.ClinicQueue.FifthDayQueue;
                type.MedicalServiceType.ClinicQueue.FifthDayQueue = type.MedicalServiceType.ClinicQueue.SixthDayQueue;
                type.MedicalServiceType.ClinicQueue.SixthDayQueue = type.MedicalServiceType.ClinicQueue.SeventhDayQueue;
                type.MedicalServiceType.ClinicQueue.SecondDayQueue = 0;
            }

            type.MedicalServiceType.ClinicQueue = CreateQueue(type.MedicalServiceType.ClinicQueue, type.BookingDate);
        }

        return entities;
    }

    private ClinicQueue CreateQueue(ClinicQueue queue, DateOnly bookingDate)
    {
        var dayDifference = bookingDate.Day - DateOnly.FromDateTime(DateTime.Now).Day;

        var number = dayDifference switch
        {
            0 => queue.TodayQueue += 1,
            1 => queue.SecondDayQueue += 1,
            2 => queue.ThirdDayQueue += 1,
            3 => queue.FourthDayQueue += 1,
            4 => queue.FifthDayQueue += 1,
            5 => queue.SixthDayQueue += 1,
            6 => queue.SecondDayQueue += 1,
            _ => throw new ArgumentIsNotValidException($"{nameof(MedicalServiceType)} is not exists for this day")
        };

        return queue;
    }
}