using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.Service.Services.Assets;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.News;

public class NewsListService(IUnitOfWork unitOfWork, IAssetService service) : INewsListService
{
    public async Task<NewsList> CreateAsync(NewsList news)
    {
        news.Create();
        var res = await unitOfWork.NewsList.InsertAsync(news);

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
            .SelectAsQueryable(expression: user => !user.IsDeleted, isTracked: false, includes: ["Picture", "Publisher"])
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(news =>
                news.Title.ToLower().Contains(search) || news.SubTitle.ToLower().Contains(search)
                || news.TitleRu.ToLower().Contains(search.ToLower()) || news.SubTitleRu.ToLower().Contains(search.ToLower()));

        return await Task.FromResult(res);
    }

    public async Task<NewsList> GetAsync(long id)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id, includes: ["Picture", "Publisher"])
            ?? throw new NotFoundException($"News with this id is not found Id = {id}");

        return existNews;
    }

    public async Task<NewsList> UpdateAsync(long id, NewsList news)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id)
            ?? throw new NotFoundException($"News with this id is not found Id = {id}");

        existNews.Id = id;
        existNews.Title = news.Title;
        existNews.SubTitle = news.SubTitle;
        existNews.TitleRu = news.TitleRu;
        existNews.SubTitleRu = news.SubTitleRu;
        existNews.Update();

        await unitOfWork.SaveAsync();

        return existNews;
    }
}