using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.Models.Pdfs;
using HospitalApi.WebApi.Models.Users;
using System.Text.Json.Serialization;

namespace HospitalApi.WebApi.Models.Laboratories;

public class LaboratoryViewModel
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public long ClientId { get; set; }
    public UserViewModel Client { get; set; }

    public long StaffId { get; set; }
    public UserViewModel Staff { get; set; }

    public long? PdfDetailsId { get; set; }
    public PdfDetailsViewModel PdfDetails { get; set; }

    public long TableId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LaboratoryTableType LaboratoryTableType { get; set; }
}
