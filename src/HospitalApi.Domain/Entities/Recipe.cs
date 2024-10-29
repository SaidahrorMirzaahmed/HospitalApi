using HospitalApi.Domain.Commons;

namespace HospitalApi.Domain.Entities;

public class Recipe : Auditable
{
    public long StaffId { get; set; }
    public User Staff { get; set; }
    public long ClientId { get; set; }
    public User Client { get; set; }
    public long? PictureId { get; set; }
    public Asset? Picture { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public DateOnly DateOfVisit { get; set; }
    public DateOnly? DateOfReturn { get; set; }
}