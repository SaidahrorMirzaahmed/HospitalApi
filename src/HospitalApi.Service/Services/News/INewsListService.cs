using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.News;

public interface INewsListService
{
    Task<NewsList> CreateAsync(NewsList news);
    Task<NewsList> UpdateAsync(long id, NewsList news);
    Task<bool> DeleteAsync(long id);
    Task<NewsList> GetAsync(long id);
    Task<IEnumerable<NewsList>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}