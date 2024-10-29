using HospitalApi.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HospitalApi.Service.Services.Assets;

public interface IAssetService
{
    Task<Asset> UploadAsync(IFormFile file);
    Task<bool> DeleteAsync(long id);
    Task<Asset> GetByIdAsync(long id);
}