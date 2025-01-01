using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Diagnosis : Auditable
{
    public string Code { get; set; }

    public string Title { get; set; }

    public string TitleRu { get; set; }
}