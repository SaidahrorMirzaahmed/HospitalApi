using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Notes;

public class NoteService(IUnitOfWork unitOfWork) : INoteService
{
    public async Task<Note> CreateAsync(Note note)
    {
        note.Create();
        return await unitOfWork.Notes.InsertAsync(note);
    }

    public async Task<Note> UpdateAsync(long id, Note note)
    {
        var entity = await unitOfWork.Notes.SelectAsync(note => note.Id == id && !note.IsDeleted)
            ?? throw new NotFoundException($"Note is not exists with id = {id}");
        entity.Description = note.Description;

        entity.Update();

        await unitOfWork.SaveAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await unitOfWork.Notes.SelectAsync(note => note.Id == id && !note.IsDeleted)
            ?? throw new NotFoundException($"Note is not exists with id = {id}");

        entity.Delete();
        await unitOfWork.Notes.DeleteAsync(entity);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<Note> GetAsync(long id)
    {
        var entity = await unitOfWork.Notes.SelectAsync(note => note.Id == id && !note.IsDeleted)
            ?? throw new NotFoundException($"Note is not exists with id = {id}");

        return entity;
    }

    public async Task<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var entities = unitOfWork.Notes.SelectAsQueryable(entity => !entity.IsDeleted, isTracked: false)
            .OrderBy(filter);

        if (search is not null)
            entities = entities.Where(entity => entity.Description.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(entities.ToPaginateAsQueryable(@params));
    }
}