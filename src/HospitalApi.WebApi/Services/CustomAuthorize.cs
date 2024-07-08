using HospitalApi.Domain.Enums;
using HospitalApi.Service.Exceptions;
using HospitalApi.WebApi.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public CustomAuthorize(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var actionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

        var allowAnonymous = actionDescriptor?.MethodInfo.GetCustomAttributes(inherit: true)
                .OfType<AllowAnonymousAttribute>().Any() ?? false;
        if (allowAnonymous) return;

        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated || !_roles.Any(user.IsInRole))
        {
            SetStatusCodeResult(context);
            return;
        }
    }

    private void SetStatusCodeResult(AuthorizationFilterContext context)
    {
        var exception = new CustomException("You do not have permission for this method", 403);
        context.Result = new ObjectResult(new Response
        {
            StatusCode = exception.StatusCode,
            Message = exception.Message
        })
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    public bool IsUserStaff(ClaimsPrincipal user)
    {
        var roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        return user.Claims.Any(c => c.Type == roleClaimType && c.Value == nameof(UserRole.Staff));
    }

    public bool IsUserOwner(ClaimsPrincipal user)
    {
        var roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        return user.Claims.Any(c => c.Type == roleClaimType && c.Value == nameof(UserRole.Owner));
    }
}