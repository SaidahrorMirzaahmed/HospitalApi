using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class PdfDetails : Auditable
{
    public string PdfName { get; set; }

    public string PdfPath { get; set; }
}