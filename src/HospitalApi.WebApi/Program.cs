using HospitalApi.DataAccess.Contexts;
using HospitalApi.WebApi.Extensions;
using HospitalApi.WebApi.Mappers;
using HospitalApi.WebApi.Middlewares;
using HospitalApi.WebApi.RouteHelper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddControllers(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new RouteHelper())));
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection")));

builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddExceptionHandlers();
builder.Services.AddProblemDetails();
builder.Services.AddAuthorization();

builder.Services.AddMemoryCache();
builder.Services.AddDdosProtection();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidators();
builder.Services.AddServices();
builder.Services.AddApiServices();
builder.Services.AddJsonConverter();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();

app.InjectEnvironmentItems();
app.UseRouting();
app.SeedData();

app.UseMiddleware<DdosProtectionMiddleware>();

app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();