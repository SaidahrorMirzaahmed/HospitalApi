namespace HospitalApi.Service.Models;

public class MonthlyRevenueStatisticsDetailsDto
{
    public DateOnly Date { get; set; }

    public double Revenue { get; set; }
}