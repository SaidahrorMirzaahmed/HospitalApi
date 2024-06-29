using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Service.Exceptions;
using Tenge.Service.Helpers;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.Assets;

public class AssetService(IUnitOfWork unitOfWork) : IAssetService
{
    public async Task<Asset> UploadAsync(IFormFile file)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, "Photos");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, file.FileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        await fileStream.WriteAsync(bytes);

        var asset = new Asset
        {
            Path = fullPath,
            Name = file.Name,
            CreatedByUserId = HttpContextHelper.UserId
        };

        var createdAsset = await unitOfWork.Assets.InsertAsync(asset);
        await unitOfWork.SaveAsync();

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
