using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Models.News;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Bookings;

public interface IBookingApiService
{
    Task<BookingViewModel> PostAsync(BookingCreateModel createModel);
    Task<BookingViewModel> PutAsync(long id, BookingUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<BookingViewModel> GetAsync(long id);
    Task<IEnumerable<BookingViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<BookingViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}
