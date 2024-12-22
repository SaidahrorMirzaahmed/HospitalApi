using AutoMapper;
using HospitalApi.Service.Models;
using HospitalApi.Service.Services.StatisticsServices;

namespace HospitalApi.WebApi.ApiServices.StatisticsDetails;

public class StatisticsApiService(IStatisticsService service, IMapper mapper) : IStatisticsApiService
{
    public async Task<Models.Statistics.StatisticsDetailsDto> GetLastYearAllStatisticsAsync()
    {
        var statistics = await service.GetLastYearAllStatisticsAsync();

        var mapped = mapper.Map<Models.Statistics.StatisticsDetailsDto>(statistics);

        return mapped;
    }
}