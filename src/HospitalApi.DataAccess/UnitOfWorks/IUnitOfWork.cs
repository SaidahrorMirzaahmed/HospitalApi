using HospitalApi.DataAccess.Repositories;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Entities.Tables;

namespace HospitalApi.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Asset> Assets { get; }
    IRepository<Booking> Bookings { get; }
    IRepository<NewsList> NewsList { get; }
    IRepository<Recipe> Recipes { get; }
    IRepository<User> Users { get; }
    IRepository<Laboratory> Laboratories { get; }
    IRepository<MedicalServiceType> MedicalServiceTypes { get; }
    // Tables
    IRepository<AnalysisOfFecesTable> AnalysisOfFecesTables { get; }
    IRepository<BiochemicalAnalysisOfBloodTable> BiochemicalAnalysisOfBloodTables { get; }
    IRepository<CommonAnalysisOfBloodTable> CommonAnalysisOfBloodTables { get; }
    IRepository<CommonAnalysisOfUrineTable> CommonAnalysisOfUrineTable { get; }
    IRepository<TorchTable> TorchTables { get; }
    Task<bool> SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}