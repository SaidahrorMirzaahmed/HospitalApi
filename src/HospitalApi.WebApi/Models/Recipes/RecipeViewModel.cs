using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public long Id { get; set; }
    public UserViewModel Staff { get;}
    public UserViewModel Client { get;}
    public AssetViewModel? Picture { get; }
    public string Title { get; }
    public string SubTitle { get; }
    public DateOnly Date { get; }
    public DateOnly? DateOfReturn { get; }
}
