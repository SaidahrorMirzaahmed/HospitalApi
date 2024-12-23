using HospitalApi.Domain.Entities;

namespace HospitalApi.Service.Models;

public class MedicalServiceTypeStatisticsDetailsDto
{
    public MedicalServiceType MedicalServiceType { get; set; }

    public int TotalBookingCount { get; set; }
}