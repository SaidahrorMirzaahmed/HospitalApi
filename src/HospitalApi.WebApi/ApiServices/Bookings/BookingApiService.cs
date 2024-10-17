using AutoMapper;
using HospitalApi.Domain.Entities;
using HospitalApi.Service.Services.Bookings;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Models.Bookings;
using HospitalApi.WebApi.Validations.Bookings;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.WebApi.ApiServices.Bookings;

public class BookingApiService(
    IMapper mapper,
    IBookingService service,
    BookingCreateModelValidator validations,
    BookingUpdateModelValidator validations1) : IBookingApiService
{
    public async Task<BookingViewModel> PostAsync(BookingCreateModel createModel)
    {
        await validations.EnsureValidatedAsync(createModel);
        var res = await service.CreateAsync(mapper.Map<Booking>(createModel));

        return mapper.Map<BookingViewModel>(res);
    }

    public async Task<BookingViewModel> PutAsync(long id, BookingUpdateModel createModel)
    {
        await validations1.EnsureValidatedAsync(createModel);
        var res = await service.UpdateAsync(id, mapper.Map<Booking>(createModel));

        return mapper.Map<BookingViewModel>(res);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await service.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<BookingViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var res = await service.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<BookingViewModel>>(res);
    }

    public async Task<IEnumerable<BookingViewModel>> GetAllbyUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var res = await service.GetAllByUserIdAsync(id, @params, filter, search);
        return mapper.Map<IEnumerable<BookingViewModel>>(res);
    }


    public async Task<BookingViewModel> GetAsync(long id)
    {
        var booking = await service.GetAsync(id);
        return mapper.Map<BookingViewModel>(booking);
    }
}