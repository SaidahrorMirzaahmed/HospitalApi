using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Service.Services.StatisticsServices;

public class StatisticsService(IUnitOfWork unitOfWork) : IStatisticsService
{
    public async Task<StatisticsDetailsDto> GetLastYearAllStatisticsAsync()
    {
        var result = new StatisticsDetailsDto();

        result.MedicalServiceTypeStatistics = await GetMedicalServiceTypeLastYearStatisticsAsync();
        result.TotalMedicalServiceTypesBookingCount = result.MedicalServiceTypeStatistics.Sum(entity => entity.TotalBookingCount);

        result.AnnualRevenueStatistics = await GetLastYearStatisticsAsync();
        result.TotalAnnualRevenue = result.AnnualRevenueStatistics.Sum(entity => entity.Revenue);

        result.MonthlyRevenueStatistics = await GetLastMonthStatisticsAsync();
        result.TotalMonthlyRevenue = result.MonthlyRevenueStatistics.Sum(entity => entity.Revenue);

        return result;
    }

    public async Task<IEnumerable<MedicalServiceTypeStatisticsDetailsDto>> GetMedicalServiceTypeLastYearStatisticsAsync()
    {
        var medicalServiceTypes = await unitOfWork.MedicalServiceTypes.SelectAsEnumerableAsync(entity => !entity.IsDeleted);
        var result = new List<MedicalServiceTypeStatisticsDetailsDto>();

        foreach (var serviceType in medicalServiceTypes)
        {
            var averageBookingCount = unitOfWork.MedicalServiceTypeHistories
                .SelectAsQueryable(entity => entity.MedicalServiceTypeId == serviceType.Id
                    && !entity.IsDeleted
                    && entity.QueueDate.Year == DateTime.UtcNow.Year)
                .Count();

            result.Add(new MedicalServiceTypeStatisticsDetailsDto
            {
                MedicalServiceType = serviceType,
                TotalBookingCount = averageBookingCount
            });
        }

        return result;
    }

    public async Task<IEnumerable<AnnualRevenueStatisticsDetailsDto>> GetLastYearStatisticsAsync()
    {
        var result = new List<AnnualRevenueStatisticsDetailsDto>();

        for (var i = 1; i <= DateTime.UtcNow.Month; i++)
        {
            var monthlyRevenue = unitOfWork.MedicalServiceTypeHistories
               .SelectAsQueryable(entity => !entity.IsDeleted
                    && entity.QueueDate.Year == DateTime.UtcNow.Year
                    && entity.QueueDate.Month == i)
               .Sum(entity => entity.MedicalServiceType.Price);

            result.Add(new AnnualRevenueStatisticsDetailsDto
            {
                Year = DateTime.UtcNow.Year,
                Month = i,
                Revenue = monthlyRevenue,
            });
        }

        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<MonthlyRevenueStatisticsDetailsDto>> GetLastMonthStatisticsAsync()
    {
        var result = new List<MonthlyRevenueStatisticsDetailsDto>();
        var currentDate = DateTime.UtcNow;

        for (var i = 1; i <= DateTime.UtcNow.Day; i++)
        {
            var dailyRevenue = unitOfWork.MedicalServiceTypeHistories
                .SelectAsQueryable(entity => !entity.IsDeleted 
                    && entity.QueueDate.Year == currentDate.Year 
                    && entity.QueueDate.Month == currentDate.Month
                    && entity.QueueDate.Day == i)
                .Sum(entity => entity.MedicalServiceType.Price);

            result.Add(new MonthlyRevenueStatisticsDetailsDto
            {
                Date = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, i),
                Revenue = dailyRevenue,
            });
        }

        return await Task.FromResult(result);
    }
}