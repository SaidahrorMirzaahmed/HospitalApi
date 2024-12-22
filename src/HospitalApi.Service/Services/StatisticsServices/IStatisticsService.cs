using HospitalApi.Domain.Entities;
using HospitalApi.Service.Models;

namespace HospitalApi.Service.Services.StatisticsServices;

public interface IStatisticsService
{
    Task<StatisticsDetailsDto> GetLastYearAllStatisticsAsync();

    Task<IEnumerable<MedicalServiceTypeStatisticsDetailsDto>> GetMedicalServiceTypeLastYearStatisticsAsync();

    Task<IEnumerable<AnnualRevenueStatisticsDetailsDto>> GetLastYearStatisticsAsync();

    Task<IEnumerable<MonthlyRevenueStatisticsDetailsDto>> GetLastMonthStatisticsAsync();
}