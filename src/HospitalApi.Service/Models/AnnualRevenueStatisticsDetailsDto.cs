namespace HospitalApi.Service.Models;

public class AnnualRevenueStatisticsDetailsDto
{
    public int Year { get; set; }

    public int Month { get; set; }
    
    public double Revenue { get; set; }
}