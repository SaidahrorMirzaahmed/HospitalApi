using HospitalApi.DataAccess.Repositories;
using HospitalApi.Domain.Entities;

namespace HospitalApi.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Asset> Assets { get; }
    IRepository<Booking> Bookings { get; }
    IRepository<NewsList> NewsList { get; }
    IRepository<Recipe> Recipes { get; }
    IRepository<User> Users { get; }
    Task<bool> SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}