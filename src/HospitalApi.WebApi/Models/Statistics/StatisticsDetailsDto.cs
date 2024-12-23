using HospitalApi.Service.Models;

namespace HospitalApi.WebApi.Models.Statistics;

public class StatisticsDetailsDto
{
    public int TotalMedicalServiceTypesBookingCount { get; set; }
    public IEnumerable<HospitalApi.WebApi.Models.Statistics.MedicalServiceTypeStatisticsDetailsDto> MedicalServiceTypeStatistics { get; set; }

    public double TotalAnnualRevenue { get; set; }
    public IEnumerable<AnnualRevenueStatisticsDetailsDto> AnnualRevenueStatistics { get; set; }

    public double TotalMonthlyRevenue { get; set; }
    public IEnumerable<MonthlyRevenueStatisticsDetailsDto> MonthlyRevenueStatistics { get; set; }
}