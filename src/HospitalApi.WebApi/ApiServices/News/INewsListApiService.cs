using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.News;

namespace HospitalApi.WebApi.ApiServices.News;

public interface INewsListApiService
{
    Task<NewsListViewModel> PostAsync(NewsListCreateModel createModel);
    Task<NewsListViewModel> PutAsync(long id, NewsListUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<NewsListViewModel> GetAsync(long id);
    Task<IEnumerable<NewsListViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}