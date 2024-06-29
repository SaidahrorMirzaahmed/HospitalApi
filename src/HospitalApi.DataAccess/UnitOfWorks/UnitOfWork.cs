using HospitalApi.DataAccess.Contexts;
using HospitalApi.DataAccess.Repositories;
using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalApi.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IRepository<Asset> Assets { get; }
    public IRepository<Booking> Bookings { get; }
    public IRepository<News> NewsList { get; }
    public IRepository<Recipe> Recipes { get; }
    public IRepository<User> Users { get; }

    private IDbContextTransaction transaction;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Users = new Repository<User>(this.context);
        Assets = new Repository<Asset>(this.context);
        Bookings = new Repository<Booking>(this.context);
        NewsList = new Repository<News>(this.context);
        Recipes = new Repository<Recipe>(this.context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task BeginTransactionAsync()
    {
        transaction = await this.context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}
