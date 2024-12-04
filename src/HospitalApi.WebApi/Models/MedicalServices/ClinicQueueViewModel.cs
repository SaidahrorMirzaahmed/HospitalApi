namespace HospitalApi.WebApi.Models.MedicalServices;

public class ClinicQueueViewModel
{
    public int TodayQueue { get; set; }
    public DateOnly QueueDate { get; set; }

    public int SecondDayQueue { get; set; }
    public int ThirdDayQueue { get; set; }
    public int FourthDayQueue { get; set; }
    public int FifthDayQueue { get; set; }
    public int SixthDayQueue { get; set; }
}