using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class AnalysisOfFecesTableResult : Auditable
{
    public long AnalysisOfFecesTableId { get; set; }

    public int Index { get; set; }

    public string Result { get; set; }
}