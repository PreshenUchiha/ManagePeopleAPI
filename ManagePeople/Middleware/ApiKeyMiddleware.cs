using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ManagePeople.Configuration
{
    public class ApiKeyMiddleware(
    RequestDelegate next,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ApiKeyMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Extensions =
            {
                { "traceId", Activity.Current?.Id }
            }
            };

            using var scope = serviceScopeFactory.CreateScope();

            var security = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SecurityOptions>>();

            if (!context.Request.Headers.TryGetValue(security.Value.RequestHeader, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;

                problemDetails.Detail = "Api key was not provided";

                await context.Response.WriteAsJsonAsync(problemDetails);

                logger.LogWarning(
                    "{Announcement}: Incoming request did not contain an API key",
                    "FAILED");

                return;
            }

            string apiKey = security.Value.XApiKey;

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;

                problemDetails.Detail = "Unauthorized client";

                await context.Response.WriteAsJsonAsync(problemDetails);

                logger.LogWarning(
                    "{Announcement}: Incoming request contained an invalid API key",
                    "FAILED");

                return;
            }

            logger.LogInformation(
                "{Announcement}: A valid API key has been provided",
                "SUCCEEDED");

            await next(context);
        }
    }
}
