using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingLotSystem.API.DTOs.Requests;
using ParkingLotSystem.API.Interfaces;

namespace ParkingLotSystem.API.Controllers;

[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _parkingService;

    public ParkingController(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [HttpPost("park")]
    [Authorize]
    public async Task<IActionResult> ParkVehicle([FromBody] ParkVehicleRequest request)
    {
        var ticket = await _parkingService.ParkVehicleAsync(request);
        return CreatedAtAction(
            nameof(GetTicketById),
            new { ticketId = ticket.TicketId },
            ticket
        );
    }

    [HttpPut("exit/{ticketId:int}")]
    [Authorize]
    public async Task<IActionResult> ExitVehicle(int ticketId)
    {
        var ticket = await _parkingService.ExitVehicleAsync(ticketId);
        return Ok(ticket);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveVehicles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest(new { message = "Page number and page size must be greater than 0." });

        var vehicles = await _parkingService.GetActiveVehiclesAsync(pageNumber, pageSize);
        return Ok(vehicles);
    }

    [HttpGet("ticket/{ticketId:int}")]
    public async Task<IActionResult> GetTicketById(int ticketId)
    {
        var ticket = await _parkingService.GetTicketByIdAsync(ticketId);
        return Ok(ticket);
    }

    [HttpGet("history/{licensePlate}")]
    public async Task<IActionResult> GetVehicleHistory(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return BadRequest(new { message = "License plate cannot be empty." });

        var history = await _parkingService.GetVehicleHistoryAsync(licensePlate);
        return Ok(history);
    }
}
