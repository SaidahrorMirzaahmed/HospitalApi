using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Services.QueueServices;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Tickets;

public class TicketService(IUnitOfWork unitOfWork, IQueueService queueService) : ITicketService
{
    public async Task<Ticket> GetByIdAsync(long id)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories"])
            ?? throw new NotFoundException($"{nameof(Ticket)} is not exists with id = {id}");

        return entity;
    }

    public async Task<IEnumerable<Ticket>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(item => item.ClientId == clientId && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories"])
            .OrderBy(filter);

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }

    public async Task<Ticket> CreateAsync(long clientId, long medicalServiceId)
    {
        await unitOfWork.BeginTransactionAsync();

        var ticket = await unitOfWork.Tickets.InsertAsync(new Ticket { ClientId = clientId });
        await unitOfWork.SaveAsync();

        var type = await queueService.CreateQueueAsync(medicalServiceId);
        var history = new MedicalServiceTypeHistory
        {
            ClientId = clientId,
            TicketId = ticket.Id,
            MedicalServiceTypeId = type.Id,
            MedicalServiceType = type,
            Queue = type.LastQueue,
            QueueDate = type.QueueDate,
        };
        await unitOfWork.MedicalServiceTypeHistories.InsertAsync(history);
        ticket.CommonPrice = type.Price;
        ticket.MedicalServiceTypeHistories.Add(history);

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return ticket;
    }

    public async Task<Ticket> CreateAsync(long clientId, IEnumerable<long> medicalServiceIds)
    {
        await unitOfWork.BeginTransactionAsync();

        var ticket = await unitOfWork.Tickets.InsertAsync(new Ticket { ClientId = clientId });
        await unitOfWork.SaveAsync();

        var types = await queueService.CreateQueuesAsync(medicalServiceIds);
        var histories = new List<MedicalServiceTypeHistory>();

        foreach (var item in types)
        {
            histories.Add(new MedicalServiceTypeHistory
            {
                ClientId = clientId,
                TicketId = ticket.Id,
                MedicalServiceTypeId = item.Id,
                MedicalServiceType = item,
                Queue = item.LastQueue,
                QueueDate= item.QueueDate,
            });
        }
        ticket.CommonPrice = types.Sum(item => item.Price);
        ticket.MedicalServiceTypeHistories = histories;

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return ticket;
    }

    public async Task<Ticket> UpdateAsync(long id, Ticket ticket)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted)
            ?? throw new NotFoundException($"{nameof(MedicalServiceTypeHistory)} is not exists with id = {id}");

        entity.Update();
        entity.IsPaid = ticket.IsPaid;
        entity.IsCancelled = ticket.IsCancelled;

        await unitOfWork.Tickets.UpdateAsync(entity);
        await unitOfWork.SaveAsync();

        return entity;
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted, includes: ["MedicalServiceTypeHistories"])
            ?? throw new NotFoundException($"{nameof(Ticket)} is not exists with id = {id}");

        await unitOfWork.BeginTransactionAsync();

        entity.Delete();
        foreach (var history in entity.MedicalServiceTypeHistories)
        {
            history.Delete();
            await unitOfWork.MedicalServiceTypeHistories.DeleteAsync(history);
        }
        await unitOfWork.Tickets.DeleteAsync(entity);
        await unitOfWork.SaveAsync();

        return true;
    }
}