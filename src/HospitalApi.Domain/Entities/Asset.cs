using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}