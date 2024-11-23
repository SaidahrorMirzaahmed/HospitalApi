using HospitalApi.Domain.Entities;
using HospitalApi.Domain.Entities.Tables;
using HospitalApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.DataAccess.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<NewsList> NewsList { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<TorchTable> TorchTables { get; set; }
    public DbSet<TorchTableResult> TorchTableResults { get; set; }
    public DbSet<CommonAnalysisOfBloodTable> CommonAnalysisOfBloodTables { get; set; }
    public DbSet<CommonAnalysisOfBloodTableResult> CommonAnalysisOfBloodTableResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

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

        modelBuilder.Entity<Booking>()
             .HasOne(b => b.Staff) // Assuming Booking has a navigation property User
             .WithMany() // Assuming User has a collection of Bookings
             .HasForeignKey(b => b.StaffId)
             .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Recipe>()
            .HasOne(b => b.Staff) // Assuming Booking has a navigation property User
            .WithMany() // Assuming User has a collection of Bookings
            .HasForeignKey(b => b.StaffId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}