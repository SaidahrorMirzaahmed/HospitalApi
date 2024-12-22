using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Models.MedicalServices;

namespace HospitalApi.WebApi.Models.Statistics;

public class MedicalServiceTypeStatisticsDetailsDto
{
    public MedicalServiceTypeViewModel MedicalServiceType { get; set; }

    public int TotalBookingCount { get; set; }
}