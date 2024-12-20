
using HospitalApi.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalApi.Service.Services.ProtectionServices;

public class DdosProtectionService : IDdosProtectionService
{
    private readonly IMemoryCache _cache;
    private readonly int _requestLimit = 50;
    private readonly TimeSpan _timeLimit = TimeSpan.FromSeconds(10);

    public DdosProtectionService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<bool> IsRequestAllowedAsync(string ipAddress)
    {
        var cacheKey = $"request-count-{ipAddress}";
        var currentTime = DateTime.UtcNow;

        if (!_cache.TryGetValue(cacheKey, out DdosRequestInfo requestInfo))
        {
            requestInfo = new DdosRequestInfo
            {
                LastRequestTime = currentTime,
                RequestCount = 1
            };

            _cache.Set(cacheKey, requestInfo, _timeLimit);
        }

        if ((currentTime - requestInfo.LastRequestTime) > _timeLimit)
        {
            requestInfo.RequestCount = 1;
            requestInfo.LastRequestTime = currentTime;
        }
        else
            requestInfo.RequestCount++;

        if (requestInfo.RequestCount > _requestLimit)
            return false;

        _cache.Set(cacheKey, requestInfo, _timeLimit);

        return true;
    }
}