namespace HospitalApi.Service.Models;

public class CommonAnalysisOfUrineTableDto
{
    public long Id { get; set; }

    public IEnumerable<CommonAnalysisOfUrineTableResultDto> Items { get; set; }
}