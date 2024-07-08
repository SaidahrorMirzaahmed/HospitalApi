using HospitalApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenge.Service.Configurations;
using Tenge.WebApi.Configurations;

namespace HospitalApi.Service.Services.Bookings;

public interface IBookingService
{
    Task<Booking> CreateAsync(Booking news);
    Task<Booking> UpdateAsync(long id, Booking news);
    Task<bool> DeleteAsync(long id);
    Task<Booking> GetAsync(long id);
    Task<IEnumerable<Booking>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<IEnumerable<Booking>> GetAllByUserIdAsync(long id, PaginationParams @params, Filter filter, string search = null);
}
