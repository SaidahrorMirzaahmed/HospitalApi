using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.Tickets;

public interface ITicketApiService
{
    Task<IEnumerable<TicketViewModelModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);

    Task<TicketViewModelModel> GetByIdAsync(long id);

    Task<PdfDetailsViewModel> GetPdf(long id);

    Task<IEnumerable<TicketViewModelModel>> GetByClientIdAsync(long id, [FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null);

    Task<TicketViewModelModel> CreateAsync(long clientId, IEnumerable<TicketCreateModel> ticketCreateModels);

    Task<TicketViewModelModel> UpdateAsync(long id);

    Task<bool> DeleteByIdAsync(long id);
}