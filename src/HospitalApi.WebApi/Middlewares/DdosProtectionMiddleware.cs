using HospitalApi.Service.Services.ProtectionServices;

namespace HospitalApi.WebApi.Middlewares;

public class DdosProtectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDdosProtectionService _ddosProtectionService;

    public DdosProtectionMiddleware(RequestDelegate next, IDdosProtectionService ddosProtectionService)
    {
        _next = next;
        _ddosProtectionService = ddosProtectionService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress.ToString();

        if (ipAddress is null || !await _ddosProtectionService.IsRequestAllowedAsync(ipAddress))
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Too many requests. Please try again later.");
            return;
        }

        await _next(context);
    }
}