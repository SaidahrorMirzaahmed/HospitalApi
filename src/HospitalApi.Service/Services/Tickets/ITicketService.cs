﻿using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Service.Services.Tickets;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    Task<Ticket> GetByIdAsync(long id);

    Task<IEnumerable<Ticket>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null);

    Task<Ticket> CreateAsync(long clientId, long medicalServiceId);

    Task<Ticket> CreateAsync(long clientId, IEnumerable<long> medicalServiceIds);

    Task<Ticket> UpdateAsync(long id, Ticket ticket);

    Task<bool> DeleteByIdAsync(long id);
}