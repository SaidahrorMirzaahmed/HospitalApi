namespace HospitalApi.WebApi.Models.Tables;

public class CommonAnalysisOfUrineTableViewModel
{
    public long Id { get; set; }

    public IEnumerable<CommonAnalysisOfUrineTableResultViewModel> Items { get; set; }
}