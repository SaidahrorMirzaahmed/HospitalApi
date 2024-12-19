namespace HospitalApi.Service.Models;

public class TorchTableDto
{
    public long Id { get; set; }

    public IEnumerable<TorchTableResultDto> Items { get; set; }
}