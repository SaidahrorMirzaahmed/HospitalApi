using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Notes;

public interface INoteService
{
    Task<Note> CreateAsync(Note note);
    Task<Note> UpdateAsync(long id, Note note);
    Task<bool> DeleteAsync(long id);
    Task<Note> GetAsync(long id);
    Task<IEnumerable<Note>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}