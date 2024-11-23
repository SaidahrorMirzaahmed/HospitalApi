using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class CommonAnalysisOfBloodTable : Auditable
{
    public ICollection<CommonAnalysisOfBloodTableResult> Items { get; set; } = new List<CommonAnalysisOfBloodTableResult>();
}