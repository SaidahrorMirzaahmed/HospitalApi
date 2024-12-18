using HospitalApi.Domain.Commons;
using HospitalApi.Domain.Enums;

namespace HospitalApi.Domain.Entities;

public class Laboratory : Auditable
{
    public long ClientId { get; set; }
    public User Client { get; set; }

    public long StaffId { get; set; }
    public User Staff { get; set; }

    public long? RecipeId { get; set; }

    public long TableId { get; set; }
    public LaboratoryTableType LaboratoryTableType { get; set; }
}