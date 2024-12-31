namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeCreateModel
{
    public long ClientId { get; set; }
    public string Complaints { get; set; }
    public long DiagnosisId { get; set; }
    public IEnumerable<long> CheckUps { get; set; }
    public string Recommendations { get; set; }
}