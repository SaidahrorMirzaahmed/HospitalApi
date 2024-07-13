using AutoMapper;
using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Services.Assets;
using HospitalApi.Service.Services.News;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.News;
using HospitalApi.WebApi.Validations.News;
using Tenge.Service.Configurations;
using Tenge.Service.Helpers;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.News;

public class NewsListApiService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IAssetService assetService,
    INewsListService service,
    NewsListCreateModelValidator validations,
    NewsListUpdateModelValidator validations1) : INewsListApiService
{
    public async Task<NewsListViewModel> PostAsync(NewsListCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();
        var mappedNews = mapper.Map<NewsList>(createModel);
        mappedNews.PublisherId = HttpContextHelper.UserId;

        var res = await service.CreateAsync(mappedNews);
        var asset = await assetService.UploadAsync(createModel.Picture);

        res.PictureId = asset.Id;
        res.Picture = asset;
        
        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<NewsListViewModel>(res);
    }

    public async Task<NewsListViewModel> PutAsync(long id, NewsListUpdateModel createModel)
    {
        await validations1.EnsureValidatedAsync(createModel);
        await unitOfWork.BeginTransactionAsync();

        var mappedNews = mapper.Map<NewsList>(createModel);
        var updatedNews = await service.UpdateAsync(id, mappedNews);

        var asset = await assetService.UploadAsync(createModel.Picture);

        updatedNews.PictureId = asset.Id;
        updatedNews.Picture = asset;

        await unitOfWork.CommitTransactionAsync();
        await unitOfWork.SaveAsync();

        return mapper.Map<NewsListViewModel>(updatedNews);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await service.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<NewsListViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var newsList = await service.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<NewsListViewModel>>(newsList);
    }

    public async Task<NewsListViewModel> GetAsync(long id)
    {
        return mapper.Map<NewsListViewModel>(await service.GetAsync(id));
    }
}