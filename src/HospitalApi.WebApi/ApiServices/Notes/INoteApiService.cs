using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Notes;

namespace HospitalApi.WebApi.ApiServices.Notes;

public interface INoteApiService
{
    Task<NoteViewModel> CreateAsync(NoteCreateModel note);
    Task<NoteViewModel> UpdateAsync(long id, NoteUpdateModel note);
    Task<bool> DeleteAsync(long id);
    Task<NoteViewModel> GetAsync(long id);
    Task<IEnumerable<NoteViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}