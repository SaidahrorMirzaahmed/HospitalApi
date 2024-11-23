using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities.Tables;

public class TorchTable : Auditable
{
    public ICollection<TorchTableResult> Items { get; set; } = new List<TorchTableResult>();
}