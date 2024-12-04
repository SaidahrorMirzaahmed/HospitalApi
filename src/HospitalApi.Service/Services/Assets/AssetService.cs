using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using HospitalApi.Service.Helpers;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Assets;

public class AssetService(IUnitOfWork unitOfWork) : IAssetService
{
    public async Task<Asset> UploadAsync(IFormFile file)
    {
        var folderName = "Uploads";
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, folderName);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(directoryPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);

        var asset = new Asset
        {
            Path = $"{folderName}//{fileName}",
            Name = fileName,
            CreatedByUserId = HttpContextHelper.UserId
        };

        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);

        return createdAsset;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this ID={id}");

        await unitOfWork.Assets.DeleteAsync(asset);
        return true;
    }

    public async Task<Asset> GetByIdAsync(long id)
    {
        var asset = await unitOfWork.Assets.SelectAsync(asset => asset.Id == id)
            ?? throw new NotFoundException($"Asset is not found with this ID={id}");

        return asset;
    }
}