using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class CommonAnalysisOfBloodTableResult : Auditable
{
    public long CommonAnalysisOfBloodTableId { get; set; }

    public int Index { get; set; }

    public string Result { get; set; }
}