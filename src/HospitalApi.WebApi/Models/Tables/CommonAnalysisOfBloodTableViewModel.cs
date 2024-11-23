namespace HospitalApi.WebApi.Models.Tables;

public class CommonAnalysisOfBloodTableViewModel
{
    public long Id { get; set; }
    
    public IEnumerable<CommonAnalysisOfBloodTableResultViewModel> Items { get; set; }
}