using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sample_test02.Context;
using sample_test02.DTOs;
using sample_test02.Models;
using sample_test02.Services;

namespace sample_test02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ReservationService _reservationService;

    public ReservationController(AppDbContext context, ReservationService reservationService)
    {
        _context = context;
        _reservationService = reservationService;
    }

    [HttpGet("clients/{id}")]
    public async Task<IActionResult> GetClientReservations(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Reservations)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
        {
            return NotFound();
        }

        var sortedReservations = client.Reservations.OrderByDescending(r => r.DateTo).ToList();

        return Ok(new { client, Reservations = sortedReservations });
    }

    [HttpPost("reservations")]
    public async Task<IActionResult> AddReservation(AddReservationDto dto)
    {
        var client = await _context.Clients
            .Include(c => c.Reservations)
            .FirstOrDefaultAsync(c => c.Id == dto.IdClient);

        if (client == null)
        {
            return NotFound("Client not found");
        }

        if (client.Reservations.Any(r => !r.Fulfilled))
        {
            return BadRequest("Client already has an active reservation");
        }

        var availableBoats = await _reservationService.GetAvailableBoats(dto.DateFrom, dto.DateTo, dto.IdBoatStandard);

        if (availableBoats < dto.NumOfBoats)
        {
            if (!_reservationService.TryUpgradeBoatStandard(dto.IdBoatStandard, dto.NumOfBoats - availableBoats, out var newBoatStandardId))
            {
                return BadRequest("Not enough boats available");
            }
            dto.IdBoatStandard = newBoatStandardId;
        }

        var reservation = new Reservation
        {
            IdClient = dto.IdClient,
            DateFrom = dto.DateFrom,
            DateTo = dto.DateTo,
            IdBoatStandard = dto.IdBoatStandard,
            NumOfBoats = dto.NumOfBoats,
            Fulfilled = false,
            Price = _reservationService.CalculatePrice(dto.IdClient, dto.IdBoatStandard, dto.NumOfBoats)
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClientReservations), new { id = reservation.Id }, reservation);
    }
}
