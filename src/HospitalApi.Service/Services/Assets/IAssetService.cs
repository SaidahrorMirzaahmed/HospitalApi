using HospitalApi.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HospitalApi.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(IFormFile file);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Asset> GetByIdAsync(long id);
}
