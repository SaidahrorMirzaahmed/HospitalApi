using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class CommonAnalysisOfUrineTableResult : Auditable
{
    public long CommonAnalysisOfUrineTableId { get; set; }

    public int Index { get; set; }

    public string Result { get; set; }
}