using Arcana.Service.Extensions;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Service.Configurations;
using Tenge.Service.Exceptions;
using Tenge.Service.Extensions;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.News;

public class NewsListService(IUnitOfWork unitOfWork) : INewsListService
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
            .SelectAsQueryable(expression: user => !user.IsDeleted, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            res = res.Where(user =>
                user.Text.ToLower().Contains(search));

        return await Task.FromResult(res);
    }

    public async Task<NewsList> GetAsyncAsync(long id)
    {
        var existNews = await unitOfWork.NewsList.SelectAsync(x => !x.IsDeleted && x.Id == id)
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
