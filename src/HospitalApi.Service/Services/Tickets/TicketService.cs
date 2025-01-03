﻿using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.QueueServices;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalApi.Service.Services.Tickets;

public class TicketService(IUnitOfWork unitOfWork, IQueueService queueService, IPdfGeneratorService pdfGeneratorService, IUserService userService) : ITicketService
{
    public async Task<IEnumerable<Ticket>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(entity => !entity.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities.Where(entity =>
                entity.Client.FirstName.ToLower().Contains(search.ToLower()) || entity.Client.LastName.ToLower().Contains(search.ToLower())
            || entity.Client.Phone.Contains(search) || entity.Client.Address.ToLower().Contains(search));

        return await entities.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAllPaidAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(entity => entity.IsPaid && !entity.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities.Where(entity =>
                entity.Client.FirstName.ToLower().Contains(search.ToLower()) || entity.Client.LastName.ToLower().Contains(search.ToLower())
            || entity.Client.Phone.Contains(search) || entity.Client.Address.ToLower().Contains(search));

        return await entities.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAllUnpaidAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(entity => !entity.IsPaid && !entity.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            entities = entities.Where(entity =>
                entity.Client.FirstName.ToLower().Contains(search.ToLower()) || entity.Client.LastName.ToLower().Contains(search.ToLower())
            || entity.Client.Phone.Contains(search) || entity.Client.Address.ToLower().Contains(search));

        return await entities.ToPaginateAsQueryable(@params).ToListAsync();
    }


    public async Task<Ticket> GetByIdAsync(long id)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            ?? throw new NotFoundException($"{nameof(Ticket)} is not exists with id = {id}");

        return entity;
    }

    public async Task<IEnumerable<Ticket>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(item => item.ClientId == clientId && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }

    public async Task<IEnumerable<Ticket>> GetPaidByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(item => item.ClientId == clientId && item.IsPaid && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }

    public async Task<IEnumerable<Ticket>> GetUnpaidByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Tickets
            .SelectAsQueryable(item => item.ClientId == clientId && !item.IsPaid && !item.IsDeleted, includes: ["Client", "MedicalServiceTypeHistories", "PdfDetails"])
            .OrderBy(filter);

        return await Task.FromResult(entities.ToPaginateAsEnumerable(@params));
    }

    public async Task<Ticket> CreateAsync(long clientId, IEnumerable<TicketCreateDto> dtos)
    {
        var client = await unitOfWork.Users.SelectAsync(entity => entity.Id == clientId && !entity.IsDeleted)
            ?? throw new NotFoundException($"{nameof(User)} is not exists id = {clientId}");

        await unitOfWork.BeginTransactionAsync();

        var ticket = new Ticket { ClientId = client.Id, Client = client };
        ticket.Create();
        if (HttpContextHelper.HttpContext.User.Claims
            .Any(c => c.Type == ClaimTypes.Role &&
                (c.Value == nameof(UserRole.Staff) || c.Value == nameof(UserRole.Owner)))) ticket.IsPaid = true;

        await unitOfWork.Tickets.InsertAsync(ticket);
        await unitOfWork.SaveAsync();

        var types = await queueService.CreateQueuesAsync(dtos);
        var histories = new List<MedicalServiceTypeHistory>();

        foreach (var item in types)
        {
            var queue = GetCurrentQueue(item);
            histories.Add(new MedicalServiceTypeHistory
            {
                ClientId = clientId,
                TicketId = ticket.Id,
                MedicalServiceTypeId = item.MedicalServiceType.Id,
                MedicalServiceType = item.MedicalServiceType,
                Queue = queue,
                QueueDate = item.BookingDate,
            });
        }
        await unitOfWork.MedicalServiceTypeHistories.BulkInsertAsync(histories);
        ticket.CommonPrice = types.Sum(item => item.MedicalServiceType.Price);
        ticket.MedicalServiceTypeHistories = histories;

        var document = await pdfGeneratorService.CreateDocument(ticket);
        ticket.PdfDetailsId = document.Id;
        ticket.PdfDetails = document;
        ticket.PdfDetails.Create();

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return ticket;
    }

    public async Task<Ticket> UpdateAsync(long id, Ticket ticket, bool isPaid)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted, includes: ["PdfDetails"])
            ?? throw new NotFoundException($"{nameof(MedicalServiceTypeHistory)} is not exists with id = {id}");

        await unitOfWork.BeginTransactionAsync();
        entity.Update();
        entity.IsPaid = isPaid;

        var document = await pdfGeneratorService.CreateDocument(entity);
        entity.PdfDetailsId = document.Id;
        entity.PdfDetails = document;

        await unitOfWork.Tickets.UpdateAsync(entity);

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return entity;
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        var entity = await unitOfWork.Tickets.SelectAsync(item => item.Id == id && !item.IsDeleted, includes: ["MedicalServiceTypeHistories", "PdfDetails"])
            ?? throw new NotFoundException($"{nameof(Ticket)} is not exists with id = {id}");

        await unitOfWork.BeginTransactionAsync();

        entity.Delete();
        foreach (var history in entity.MedicalServiceTypeHistories)
        {
            history.Delete();
            await unitOfWork.MedicalServiceTypeHistories.DeleteAsync(history);
        }
        await unitOfWork.Tickets.DeleteAsync(entity);
        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return true;
    }

    private int GetCurrentQueue((MedicalServiceType MedicalServiceType, DateOnly BookingDate) type)
    {
        var day = type.BookingDate.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber;

        var queue = day switch
        {
            0 => type.MedicalServiceType.ClinicQueue.TodayQueue,
            1 => type.MedicalServiceType.ClinicQueue.SecondDayQueue,
            2 => type.MedicalServiceType.ClinicQueue.ThirdDayQueue,
            3 => type.MedicalServiceType.ClinicQueue.FourthDayQueue,
            4 => type.MedicalServiceType.ClinicQueue.FifthDayQueue,
            5 => type.MedicalServiceType.ClinicQueue.SixthDayQueue,
            6 => type.MedicalServiceType.ClinicQueue.SecondDayQueue,
            _ => throw new NotFoundException($"{nameof(MedicalServiceType)} is not exists for the day {type.BookingDate}")
        };

        return queue;
    }
}