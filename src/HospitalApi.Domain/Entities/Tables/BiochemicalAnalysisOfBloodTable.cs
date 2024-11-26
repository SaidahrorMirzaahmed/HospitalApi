using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class BiochemicalAnalysisOfBloodTable : Auditable
{
    public ICollection<BiochemicalAnalysisOfBloodTableResult> Items { get; set; } = new List<BiochemicalAnalysisOfBloodTableResult>();
}