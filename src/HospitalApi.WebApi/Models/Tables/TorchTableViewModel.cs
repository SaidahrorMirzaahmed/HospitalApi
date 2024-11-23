namespace HospitalApi.WebApi.Models.Tables;

public class TorchTableViewModel
{
    public long Id { get; set; }

    public IEnumerable<TorchTableResultViewModel> Items { get; set; }
}