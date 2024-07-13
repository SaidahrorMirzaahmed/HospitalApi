using HospitalApi.Domain.Entities;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeCreateModel
{
    public long ClientId { get; set; }
    public IFormFile Picture { get; set; } 
    public DateTime Date { get; set; }
}
