using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public long Id { get; set; }
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }
    public AssetViewModel? Picture { get; set; }
    public string Complaints { get; set; }
    public string Diagnosis { get; set; }
    public string CheckUps { get; set; }
    public string Recommendations { get; set; }
    public DateTime DateOfVisit { get; set; }
}