using HospitalApi.Domain.Enums;

namespace HospitalApi.WebApi.Models.Bookings;

public class BookingUpdateModel
{
    public long StaffId { get; set; }
    public long ClientId { get; set; }
    public TimesOfBooking Time { get; set; }
    public DateOnly Date { get; set; }
}