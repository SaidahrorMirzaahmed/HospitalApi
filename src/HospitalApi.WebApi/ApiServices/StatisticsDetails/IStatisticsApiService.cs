namespace HospitalApi.WebApi.ApiServices.StatisticsDetails;

public interface IStatisticsApiService
{
    public Task<Models.Statistics.StatisticsDetailsDto> GetLastYearAllStatisticsAsync();
}