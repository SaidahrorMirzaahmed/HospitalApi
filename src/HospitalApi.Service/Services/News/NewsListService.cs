using Arcana.Service.Extensions;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Services.Assets;
using Tenge.Service.Configurations;
using Tenge.Service.Extensions;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.News;

public class NewsListService(IUnitOfWork unitOfWork, IAssetService service) : INewsListService
{
    public async Task<NewsList> CreateAsync(NewsList news)
    {
        news.Create();
        var res = await unitOfWork.NewsList.InsertAsync(news);
        await unitOfWork.SaveAsync();

        return res;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id)
            ?? throw new NotFoundException($"News with this id is not found Id = {id}");

        existNews.Delete();

        await unitOfWork.NewsList.DeleteAsync(existNews);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<NewsList>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var res = unitOfWork.NewsList
            .SelectAsQueryable(expression: user => !user.IsDeleted, isTracked: false, includes: ["Picture"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(user =>
                user.Text.ToLower().Contains(search));

        return await Task.FromResult(res);
    }

    public async Task<NewsList> GetAsync(long id)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id, includes: ["Picture"])
            ?? throw new NotFoundException($"News with this id is not found Id = {id}");

        return existNews;
    }

    public async Task<NewsList> UpdateAsync(long id, NewsList news)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id)
            ?? throw new NotFoundException($"News with this id is not found Id = {id}");

        existNews.Id = id;
        existNews.Text = news.Text;
        existNews.Publisher = news.Publisher;
        existNews.PublisherId = news.PublisherId;
        existNews.Update();

        await unitOfWork.SaveAsync();

        return existNews;
    }
}
