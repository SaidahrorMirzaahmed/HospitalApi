using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class BiochemicalAnalysisOfBloodTableResult : Auditable
{
    public long BiochemicalAnalysisOfBloodTableId { get; set; }
    public BiochemicalAnalysisOfBloodTable BiochemicalAnalysisOfBloodTable { get; set; }

    public int Index { get; set; }

    public string Result { get; set; }
}