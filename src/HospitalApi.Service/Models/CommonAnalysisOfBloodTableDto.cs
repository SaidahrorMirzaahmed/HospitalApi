namespace HospitalApi.Service.Models;

public class CommonAnalysisOfBloodTableDto
{
    public long Id { get; set; }
    
    public IEnumerable<CommonAnalysisOfBloodTableResultDto> Items { get; set; }
}