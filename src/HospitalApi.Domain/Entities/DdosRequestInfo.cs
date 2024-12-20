namespace HospitalApi.Domain.Entities;

public class DdosRequestInfo
{
    public DateTime LastRequestTime { get; set; }

    public int RequestCount { get; set; }
}