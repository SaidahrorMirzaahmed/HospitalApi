﻿using Microsoft.AspNetCore.Diagnostics;
using HospitalApi.Service.Exceptions;
using HospitalApi.WebApi.Models.Responses;

namespace HospitalApi.WebApi.Middlewares;

public class AlreadyExistExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        if (exception is not AlreadyExistException alreadyExistException)
            return false;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = alreadyExistException.StatusCode,
            Message = alreadyExistException.Message,
        });

        return true;
    }
}