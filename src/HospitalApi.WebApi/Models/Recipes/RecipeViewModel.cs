using HospitalApi.Domain.Entities;
using HospitalApi.WebApi.Models.Assets;
using HospitalApi.WebApi.Models.Laboratories;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Recipes;

public class RecipeViewModel
{
    public long Id { get; set; }
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }
    public string Complaints { get; set; }
    public string Diagnosis { get; set; }
    public IEnumerable<LaboratoryViewModel> CheckUps { get; set; }
    public string Recommendations { get; set; }
}