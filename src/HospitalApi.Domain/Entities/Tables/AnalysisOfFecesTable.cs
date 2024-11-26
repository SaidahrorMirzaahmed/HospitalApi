using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class AnalysisOfFecesTable : Auditable
{
    public ICollection<AnalysisOfFecesTableResult> Items { get; set; } = new List<AnalysisOfFecesTableResult>();
}