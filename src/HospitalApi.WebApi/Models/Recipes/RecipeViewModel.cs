using HospitalApi.WebApi.Models.Diagnoses;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public long Id { get; set; }
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }

    public long PdfDetailsId { get; set; }
    public PdfDetailsViewModel PdfDetails { get; set; }

    public string Complaints { get; set; }

    public long DiagnosisId { get; set; }
    public DiagnosisViewModel Diagnosis { get; set; }

    public IEnumerable<LaboratoryViewModel> CheckUps { get; set; }
    public string Recommendations { get; set; }
}