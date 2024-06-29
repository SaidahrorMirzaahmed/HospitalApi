using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<News> NewsList { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }
}
