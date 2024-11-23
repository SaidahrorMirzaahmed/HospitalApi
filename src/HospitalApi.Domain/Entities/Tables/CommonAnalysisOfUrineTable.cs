using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class CommonAnalysisOfUrineTable : Auditable
{
    public ICollection<CommonAnalysisOfUrineTableResult> Items { get; set; } = new List<CommonAnalysisOfUrineTableResult>();

}