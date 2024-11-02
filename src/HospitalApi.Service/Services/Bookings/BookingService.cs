using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using HospitalApi.Service.Configurations;
using HospitalApi.Service.Exceptions;
using HospitalApi.Service.Extensions;
using HospitalApi.WebApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Service.Services.Bookings;

public class BookingService(IUnitOfWork unitOfWork) : IBookingService
{
    public async Task<Booking> CreateAsync(Booking booking)
    {
        var existStaff = await unitOfWork.Users.SelectAsync(s => s.Id == booking.StaffId)
            ?? throw new NotFoundException($"User with this Id is not found Id = {booking.StaffId}");
        var existClient = await unitOfWork.Users.SelectAsync(s => s.Id == booking.ClientId)
            ?? throw new NotFoundException($"User with this Id is not found Id = {booking.Client}");
        if (existStaff.Role != Domain.Enums.UserRole.Staff || existClient.Role != Domain.Enums.UserRole.Client)
            throw new ArgumentIsNotValidException("Bookings should be only from Clients to Staff");

        var alreadyExistBooking = await unitOfWork.Bookings.SelectAsync(x => !x.IsDeleted && x.Date == booking.Date && x.StaffId == booking.StaffId && x.Time == booking.Time);
        if (alreadyExistBooking is not null)
            throw new AlreadyExistException("Booking already exists");

        booking.Create();
        booking.Staff = existStaff;
        booking.Client = existClient;
        var result = await unitOfWork.Bookings.InsertAsync(booking);
        await unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new NotFoundException($"Booking with this Id is not found {id}");

        existBooking.Delete();
        await unitOfWork.Bookings.DeleteAsync(existBooking);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var bookings = unitOfWork.Bookings
            .SelectAsQueryable(expression: x => !x.IsDeleted, isTracked: false, includes: ["Staff", "Client"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            bookings = bookings.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search) || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search));

        return await Task.FromResult(bookings);
    }

    public async Task<IEnumerable<Booking>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null)
    {
        var bookings = unitOfWork.Bookings
            .SelectAsQueryable(expression: x => !x.IsDeleted && x.ClientId == id || x.StaffId == id, isTracked: false, includes: ["Staff", "Client"])
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            bookings = bookings.Where(x => x.Staff.FirstName.Contains(search) || x.Staff.LastName.Contains(search) || x.Client.FirstName.Contains(search) || x.Client.LastName.Contains(search));

        return await Task.FromResult(bookings);
    }

    public async Task<Booking> GetAsync(long id)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client"])
           ?? throw new NotFoundException($"Booking with this Id is not found {id}");

        return existBooking;
    }

    public async Task<Booking> UpdateAsync(long id, Booking booking)
    {
        var existBooking = await unitOfWork.Bookings.SelectAsync(x => x.Id == id && !x.IsDeleted, includes: ["Staff", "Client"])
           ?? throw new NotFoundException($"Booking with this Id is not found {id}");

        var alreadyExistBooking = await unitOfWork.Bookings.SelectAsync(x => !x.IsDeleted && x.Date == booking.Date && x.StaffId == booking.StaffId && x.Time == booking.Time);
        if (alreadyExistBooking is not null && alreadyExistBooking.ClientId != existBooking.ClientId)
            throw new AlreadyExistException("Booking already exists");

        existBooking.Id = id;
        existBooking.Date = booking.Date;
        existBooking.Time = booking.Time;


        await unitOfWork.SaveAsync();

        return existBooking;
    }

    public async Task<IEnumerable<(User, IEnumerable<TimesOfBooking>)>> GetByDateAsync(DateOnly date, PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users
            .SelectAsQueryable(expression: user => !user.IsDeleted && user.Role == UserRole.Staff, isTracked: false);

        var bookings = unitOfWork.Bookings.SelectAsQueryable(booking => booking.Date == date, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            users = users
                .Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.LastName.ToLower().Contains(search.ToLower()) ||
                    x.FirstName.ToLower().Contains(search.ToLower()) ||
                    x.LastName.ToLower().Contains(search.ToLower())
                );

        var result = new List<(User, IEnumerable<TimesOfBooking>)>();

        foreach (var user in users)
        {
            var list = bookings
                .Where(booking => booking.StaffId == user.Id)
                .Select(booking => booking.Time).AsEnumerable();

            result.Add((user, list));
        }

        return result.AsEnumerable();
    }
}