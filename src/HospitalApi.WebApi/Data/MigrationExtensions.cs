using HospitalApi.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.WebApi.Data;

public static class MigrationExtensions
{
    public static async ValueTask MigrationAsync(this IServiceProvider provider)
    {
        var context = provider.GetRequiredService<AppDbContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
            await context.Database.MigrateAsync();
    }
}