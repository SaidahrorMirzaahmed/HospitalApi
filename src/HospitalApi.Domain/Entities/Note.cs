using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Note : Auditable
{
    public string Description { get; set; }
}