using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Models;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Tickets;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    Task<IEnumerable<Ticket>> GetAllPaidAsync(PaginationParams @params, Filter filter, string search = null);

    Task<IEnumerable<Ticket>> GetAllUnpaidAsync(PaginationParams @params, Filter filter, string search = null);

    Task<Ticket> GetByIdAsync(long id);

    Task<IEnumerable<Ticket>> GetByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null);

    Task<IEnumerable<Ticket>> GetPaidByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null);

    Task<IEnumerable<Ticket>> GetUnpaidByClientIdAsync(long clientId, PaginationParams @params, Filter filter, string search = null);

    Task<Ticket> CreateAsync(long clientId, IEnumerable<TicketCreateDto> createDtos);

    Task<Ticket> UpdateAsync(long id, Ticket ticket, bool isPaid);

    Task<bool> DeleteByIdAsync(long id);
}