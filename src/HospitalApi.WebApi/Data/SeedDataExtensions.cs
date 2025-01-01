using HospitalApi.DataAccess.Contexts;
using HospitalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HospitalApi.WebApi.Data;

public static class SeedDataExtensions
{
    public static async ValueTask SeedDataAsync(this IServiceProvider provider)
    {
        var context = provider.GetRequiredService<AppDbContext>();
        var webHostEnvironment = provider.GetRequiredService<IWebHostEnvironment>();

        if (!await context.Diagnoses.AnyAsync())
            await SeedDiagnosisAsync(context, webHostEnvironment);
    }

    private static async ValueTask SeedDiagnosisAsync(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        var diagnosesFileName = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "SeedData", "Diagnoses.json");
        var diagnosesData = JsonConvert.DeserializeObject<List<Diagnosis>>(await File.ReadAllTextAsync(diagnosesFileName));

        await dbContext.Diagnoses.AddRangeAsync(diagnosesData);
        await dbContext.SaveChangesAsync();
    }
}