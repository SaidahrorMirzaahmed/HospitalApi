namespace HospitalApi.WebApi.Models.Tables;

public class AnalysisOfFecesTableViewModel
{
    public long Id { get; set; }

    public IEnumerable<AnalysisOfFecesTableResultViewModel> Items { get; set; }
}