using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class TorchTableResult : Auditable
{
    public long TorchTableId { get; set; }

    public int Index { get; set; }

    public string Result { get; set; }
}