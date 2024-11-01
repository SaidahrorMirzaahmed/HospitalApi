using HospitalApi.Service.Configurations;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Models.Bookings;

namespace HospitalApi.WebApi.ApiServices.Bookings;

public interface IBookingApiService
{
    Task<BookingViewModel> PostAsync(BookingCreateModel createModel);
    Task<BookingViewModel> PutAsync(long id, BookingUpdateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<BookingViewModel> GetAsync(long id);
    Task<IEnumerable<BookingViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<BookingViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<BookingViewModelByDate>> GetByDateAsync(DateOnly date, PaginationParams @params, Filter filter, string search = null);
}
