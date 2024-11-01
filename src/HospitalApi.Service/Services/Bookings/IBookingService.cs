using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;

namespace HospitalApi.Service.Services.Bookings;

public interface IBookingService
{
    Task<Booking> CreateAsync(Booking news);
    Task<Booking> UpdateAsync(long id, Booking news);
    Task<bool> DeleteAsync(long id);
    Task<Booking> GetAsync(long id);
    Task<IEnumerable<Booking>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<Booking>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<(User, IEnumerable<TimesOfBooking>)>> GetByDateAsync(DateOnly date, PaginationParams @params, Filter filter, string search = null);
}