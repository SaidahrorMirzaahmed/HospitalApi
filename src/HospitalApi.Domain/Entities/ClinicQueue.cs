using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class ClinicQueue : Auditable
{
    public long MedicalServiceTypeId { get; set; }
    public MedicalServiceType MedicalServiceType { get; set; }

    public int TodayQueue { get; set; }
    public DateOnly QueueDate { get; set; }

    public int SecondDayQueue { get; set; }
    public int ThirdDayQueue { get; set; }
    public int FourthDayQueue { get; set; }
    public int FifthDayQueue { get; set; }
    public int SixthDayQueue { get; set; }
    public int SeventhDayQueue { get; set; }
}