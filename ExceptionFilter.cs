using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Log the error
        _logger.LogError(exception, "Unhandled exception occurred in {Path}", context.HttpContext.Request.Path);

        // Create a generic error response
        var problemDetails = new ProblemDetails
        {
            Status = 500,
            Title = "An unexpected error occurred.",
            Detail = "Please contact support if the problem persists.",
            Instance = context.HttpContext.TraceIdentifier
        };

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}






public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>(); // Register global exception filter
    });

    services.AddScoped<GlobalExceptionFilter>();
}
