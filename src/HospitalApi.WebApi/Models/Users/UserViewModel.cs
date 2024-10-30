using HospitalApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace HospitalApi.WebApi.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserRole Role { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserSpecialists MedicalSpecialists { get; set; }
    public string Phone { get; set; }
}