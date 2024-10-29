using HospitalApi.Domain.Commons;
using HospitalApi.Domain.Enums;

namespace HospitalApi.Domain.Entities;

public class Booking : Auditable
{
    public long StaffId { get; set; }
    public User Staff { get; set; }
    public long ClientId { get; set; }
    public User Client { get; set; }
    public TimesOfBooking Time { get; set; }
    public DateOnly Date { get; set; }
}