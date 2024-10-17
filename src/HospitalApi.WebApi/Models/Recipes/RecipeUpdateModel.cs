namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeUpdateModel
{
    public long ClientId { get; set; }
    public IFormFile? Picture { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly? DateOfReturn { get; set; } 
}
