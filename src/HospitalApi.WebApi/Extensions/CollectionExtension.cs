using HospitalApi.DataAccess.UnitOfWorks;
using HospitalApi.Service.Helpers;
using HospitalApi.Service.Services.Assets;
using HospitalApi.Service.Services.Bookings;
using HospitalApi.Service.Services.News;
using HospitalApi.Service.Services.Notifications;
using HospitalApi.Service.Services.Recipes;
using HospitalApi.Service.Services.Users;
using HospitalApi.WebApi.ApiServices.Accounts;
using HospitalApi.WebApi.ApiServices.Bookings;
using HospitalApi.WebApi.ApiServices.News;
using HospitalApi.WebApi.ApiServices.Recipes;
using HospitalApi.WebApi.ApiServices.Users;
using HospitalApi.WebApi.Configurations;
using HospitalApi.WebApi.Middlewares;
using HospitalApi.WebApi.Validations.Bookings;
using HospitalApi.WebApi.Validations.News;
using HospitalApi.WebApi.Validations.Recipes;
using HospitalApi.WebApi.Validations.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HospitalApi.WebApi.Extensions;

public static class CollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<INewsListService, NewsListService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IBookingService, BookingService>();
    }

    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountApiService, AccountApiService>();
        services.AddScoped<IUserApiService, UserApiService>();
        services.AddScoped<INewsListApiService, NewsListApiService>();
        services.AddScoped<IRecipeApiService, RecipeApiService>();
        services.AddScoped<IBookingApiService, BookingApiService>();
        services.AddScoped<ICodeSenderService, CodeSenderService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistExceptionHandler>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionHandler>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<InternalServerExceptionHandler>();
    }
    public static void InjectEnvironmentItems(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
        EnvironmentHelper.JWTKey = app.Configuration.GetSection("JWT:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHours = app.Configuration.GetSection("JWT:LifeTime").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
        EnvironmentHelper.PageSize = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageSize").Value);
        EnvironmentHelper.PageIndex = Convert.ToInt32(app.Configuration.GetSection("PaginationParams:PageIndex").Value);
        EnvironmentHelper.CodeSenderBotToken = app.Configuration.GetSection("CodeSenderBot:Token").Value;
        EnvironmentHelper.CodeSenderBotReceiverChatId = app.Configuration.GetSection("CodeSenderBot:ChatId").Value;
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<BookingCreateModelValidator>();
        services.AddTransient<BookingUpdateModelValidator>();

        services.AddTransient<NewsListCreateModelValidator>();
        services.AddTransient<NewsListUpdateModelValidator>();

        services.AddTransient<RecipeCreateModelValidator>();
        services.AddTransient<RecipeUpdateModelValidator>();

        services.AddTransient<UserCreateModelValidator>();
        services.AddTransient<UserUpdateModelValidator>();
    }
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}