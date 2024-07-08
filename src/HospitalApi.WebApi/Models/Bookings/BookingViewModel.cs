using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Bookings;

public class BookingViewModel
{
    public long Id { get; set; }   
    public UserViewModel Staff { get; set; }
    public UserViewModel Client { get; set; }
    public TimesOfBooking Time { get; set; }
    public DateOnly Date { get; set; }
}


