using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }
    public AssetViewModel Picture { get; set; }
    public DateTime Date { get; set; }
}
