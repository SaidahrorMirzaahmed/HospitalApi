using HospitalApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace HospitalApi.WebApi.Models.Laboratories;

public class LaboratoryUpdateModel
{
    public long ClientId { get; set; }

    public LaboratoryTableType LaboratoryTableType { get; set; }
}