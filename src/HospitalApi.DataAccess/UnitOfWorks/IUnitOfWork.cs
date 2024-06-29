using HospitalApi.DataAccess.Repositories;
using HospitalApi.Domain.Entities;

namespace HospitalApi.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Asset> Assets { get; }
    IRepository<Booking> Bookings { get; }
    IRepository<News> NewsList { get; }
    IRepository<Recipe> Recipes { get; }
    IRepository<User> Users { get; }
    ValueTask<bool> SaveAsync();
}
