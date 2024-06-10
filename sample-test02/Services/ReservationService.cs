using Microsoft.EntityFrameworkCore;
using sample_test02.Context;

namespace sample_test02.Services;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;

    public ReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetAvailableBoats(DateTime dateFrom, DateTime dateTo, int boatStandardId)
    {
        var reservations = await _context.Reservations
            .Where(r => r.DateFrom < dateTo && r.DateTo > dateFrom && r.IdBoatStandard == boatStandardId)
            .ToListAsync();

        var reservedBoats = reservations.Sum(r => r.NumOfBoats);

        var boatStandard = await _context.BoatStandards.FindAsync(boatStandardId);

        return boatStandard.AvailableBoats - reservedBoats;
    }

    public bool TryUpgradeBoatStandard(int boatStandardId, int additionalBoatsNeeded, out int newBoatStandardId)
    {
        var higherStandards = _context.BoatStandards
            .Where(b => b.Level > _context.BoatStandards.Find(boatStandardId).Level)
            .OrderBy(b => b.Level)
            .ToList();

        foreach (var standard in higherStandards)
        {
            var availableBoats = GetAvailableBoats(DateTime.Now, DateTime.Now, standard.Id).Result;
            if (availableBoats >= additionalBoatsNeeded)
            {
                newBoatStandardId = standard.Id;
                return true;
            }
        }

        newBoatStandardId = 0;
        return false;
    }

    public decimal CalculatePrice(int clientId, int boatStandardId, int numOfBoats)
    {
        var basePrice = _context.BoatStandards.Find(boatStandardId).Level * 100; // Example price calculation
        var totalPrice = basePrice * numOfBoats;

        var client = _context.Clients.Include(c => c.Promotions).FirstOrDefault(c => c.Id == clientId);

        if (client != null)
        {
            foreach (var promo in client.Promotions)
            {
                totalPrice -= totalPrice * (int)promo.Discount;
            }
        }

        return totalPrice;
    }
}