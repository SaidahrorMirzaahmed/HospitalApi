using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Services.Notes;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Notes;

namespace HospitalApi.WebApi.ApiServices.Notes;

public class NoteApiService(INoteService noteService, IUnitOfWork unitOfWork, IMapper mapper) : INoteApiService
{
    public async Task<NoteViewModel> CreateAsync(NoteCreateModel note)
    {
        await unitOfWork.BeginTransactionAsync();

        var entity = mapper.Map<Note>(note);
        var result = await noteService.CreateAsync(entity);

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<NoteViewModel>(result);
    }

    public async Task<NoteViewModel> UpdateAsync(long id, NoteUpdateModel note)
    {
        var entity = mapper.Map<Note>(note);
        var result = await noteService.UpdateAsync(id, entity);

        return mapper.Map<NoteViewModel>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var result = await noteService.DeleteAsync(id);

        return result;
    }

    public async Task<IEnumerable<NoteViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = await noteService.GetAllAsync(@params, filter, search);

        return mapper.Map<IEnumerable<NoteViewModel>>(entities);
    }

    public async Task<NoteViewModel> GetAsync(long id)
    {
        var entity = await noteService.GetAsync(id);

        return mapper.Map<NoteViewModel>(entity);
    }
}