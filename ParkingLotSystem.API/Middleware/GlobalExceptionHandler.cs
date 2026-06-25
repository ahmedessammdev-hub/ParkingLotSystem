using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.API.Exceptions;
using System.Net;
using System.Text.Json;

namespace ParkingLotSystem.API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            NoSpotAvailableException      => (HttpStatusCode.Conflict,  "No Spot Available"),
            TicketNotFoundException        => (HttpStatusCode.NotFound,  "Ticket Not Found"),
            TicketAlreadyClosedException   => (HttpStatusCode.BadRequest, "Ticket Already Closed"),
            SpotAlreadyOccupiedException   => (HttpStatusCode.Conflict,  "Spot Already Occupied"),
            SpotAlreadyFreeException       => (HttpStatusCode.BadRequest, "Spot Already Free"),
            ArgumentException              => (HttpStatusCode.BadRequest, "Invalid Argument"),
            _                              => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };

        var problemDetails = new ProblemDetails
        {
            Status   = (int)statusCode,
            Title    = title,
            Detail   = exception.Message,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode  = (int)statusCode;

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
