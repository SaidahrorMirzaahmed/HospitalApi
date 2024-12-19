namespace HospitalApi.Service.Models;

public class AnalysisOfFecesTableDto
{
    public long Id { get; set; }

    public IEnumerable<AnalysisOfFecesTableResultDto> Items { get; set; }
}