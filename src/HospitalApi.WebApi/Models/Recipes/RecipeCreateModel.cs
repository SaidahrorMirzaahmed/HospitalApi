namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeCreateModel
{
    public long ClientId { get; set; }
    public IFormFile? Picture { get; set; }
    public string Complaints { get; set; }
    public string Diagnosis { get; set; }
    public string CheckUps { get; set; }
    public string Recommendations { get; set; }
}