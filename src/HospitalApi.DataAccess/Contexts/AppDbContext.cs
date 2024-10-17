using System.Collections;
using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.DataAccess.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<NewsList> NewsList { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                CreatedAt = default,
                DeletedAt = null,
                IsDeleted = false,
                UpdatedAt = null,
                UpdatedByUserId = null,
                CreatedByUserId = 1,
                DeletedByUserId = null,
                FirstName = "Admin",
                LastName = "Admin",
                Phone = "+998906900045",
                Role = UserRole.Owner,
            });
    }
}
