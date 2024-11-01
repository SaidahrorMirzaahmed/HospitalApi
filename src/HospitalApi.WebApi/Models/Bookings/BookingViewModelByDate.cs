using HospitalApi.Domain.Enums;
using HospitalApi.WebApi.Models.Users;

namespace HospitalApi.WebApi.Models.Bookings;

public class BookingViewModelByDate
{
    public UserViewModel UserViewModel { get; set; }

    public IEnumerable<TimesOfBooking> BookedTimes { get; set; }
}