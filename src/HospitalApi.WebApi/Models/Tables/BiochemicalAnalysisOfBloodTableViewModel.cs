namespace HospitalApi.WebApi.Models.Tables;

public class BiochemicalAnalysisOfBloodTableViewModel
{
    public long Id { get; set; }

    public IEnumerable<BiochemicalAnalysisOfBloodTableResultViewModel> Items { get; set; }
}