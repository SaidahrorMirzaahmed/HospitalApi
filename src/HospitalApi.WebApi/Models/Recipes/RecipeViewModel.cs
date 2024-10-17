using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public long Id { get; set; }
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }
    public AssetViewModel? Picture { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public DateOnly DateOfVisit { get; set; }
    public DateOnly? DateOfReturn { get; set; }
}
