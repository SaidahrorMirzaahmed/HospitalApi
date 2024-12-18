namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeUpdateModel
{
    public long ClientId { get; set; }
    public string Complaints { get; set; }
    public string Diagnosis { get; set; }
    public IEnumerable<long> CheckUps { get; set; }
    public string Recommendations { get; set; }
}