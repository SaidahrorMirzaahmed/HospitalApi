using HospitalApi.Service.Services.ProtectionServices;

namespace HospitalApi.WebApi.Middlewares;

public class DdosProtectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _provider;

    public DdosProtectionMiddleware(RequestDelegate next, IServiceProvider provider)
    {
        _next = next;
        _provider = provider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using(var scope = _provider.CreateScope())
        {
            var ddosProtectionService = scope.ServiceProvider.GetRequiredService<IDdosProtectionService>();
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            
            if (ipAddress is null || !await ddosProtectionService.IsRequestAllowedAsync(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }
        }

        await _next(context);
    }
}