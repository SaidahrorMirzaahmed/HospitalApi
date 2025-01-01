using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Recipe : Auditable
{
    public long StaffId { get; set; }
    public User Staff { get; set; }

    public long ClientId { get; set; }
    public User Client { get; set; }

    public long? PdfDetailsId { get; set; }
    public PdfDetails PdfDetails { get; set; }

    public string Complaints { get; set; }

    public long? DiagnosisId { get; set; }
    public Diagnosis Diagnosis { get; set; }

    public ICollection<Laboratory> CheckUps { get; set; }
    public string Recommendations { get; set; }
}