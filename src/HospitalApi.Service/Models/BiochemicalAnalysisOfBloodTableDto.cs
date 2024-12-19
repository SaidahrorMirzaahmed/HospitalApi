namespace HospitalApi.Service.Models;

public class BiochemicalAnalysisOfBloodTableDto
{
    public long Id { get; set; }

    public IEnumerable<BiochemicalAnalysisOfBloodTableResultDto> Items { get; set; }
}