using HospitalApi.Domain.Entities;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeCreateModel
{
    public long StaffId { get; set; }
    public long ClientId { get; set; }
    public long PictureId { get; set; }
    public DateTime Date { get; set; }
}
