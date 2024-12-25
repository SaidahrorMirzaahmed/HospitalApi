using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.PdfGeneratorServices;
using HospitalApi.Service.Services.Tickets;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.WebApi.ApiServices.Tickets;

public class TicketApiService(IUnitOfWork unitOfWork, ITicketService service, IMapper mapper, IPdfGeneratorService pdfGeneratorService) : ITicketApiService
{
    public async Task<IEnumerable<TicketViewModelModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = await service.GetAllAsync(@params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<IEnumerable<TicketViewModelModel>> GetAllPaidAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = await service.GetAllPaidAsync(@params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<IEnumerable<TicketViewModelModel>> GetAllUnpaidAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = await service.GetAllUnpaidAsync(@params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }


    public async Task<TicketViewModelModel> GetByIdAsync(long id)
    {
        var entity = await service.GetByIdAsync(id);

        return mapper.Map<TicketViewModelModel>(entity);
    }

    public async Task<PdfDetailsViewModel> GetPdf(long id)
    {
        var entity = await service.GetByIdAsync(id);
        await unitOfWork.BeginTransactionAsync();
        var pdf = await pdfGeneratorService.CreateDocument(entity);
        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<PdfDetailsViewModel>(pdf);
    }


    public async Task<IEnumerable<TicketViewModelModel>> GetByClientIdAsync(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        var entities = await service.GetByClientIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<IEnumerable<TicketViewModelModel>> GetPaidByClientIdAsync(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        var entities = await service.GetPaidByClientIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<IEnumerable<TicketViewModelModel>> GetUnpaidByClientIdAsync(long id,
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        var entities = await service.GetUnpaidByClientIdAsync(id, @params, filter, search);

        return mapper.Map<IEnumerable<TicketViewModelModel>>(entities);
    }

    public async Task<TicketViewModelModel> CreateAsync(long clientId, IEnumerable<TicketCreateModel> ticketCreateModels)
    {
        var dtos = mapper.Map<IEnumerable<TicketCreateDto>>(ticketCreateModels);
        var entity = await service.CreateAsync(clientId, dtos);

        return mapper.Map<TicketViewModelModel>(entity);
    }

    public async Task<TicketViewModelModel> UpdateAsync(long id, bool isPaid)
    {
        var updatedEntity = await service.UpdateAsync(id, null, isPaid);

        return mapper.Map<TicketViewModelModel>(updatedEntity);
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        return await service.DeleteByIdAsync(id);
    }
}