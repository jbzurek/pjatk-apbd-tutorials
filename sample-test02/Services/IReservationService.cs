namespace sample_test02.Services;

using System;
using System.Threading.Tasks;

public interface IReservationService
{
    Task<int> GetAvailableBoats(DateTime dateFrom, DateTime dateTo, int boatStandardId);
    bool TryUpgradeBoatStandard(int boatStandardId, int additionalBoatsNeeded, out int newBoatStandardId);
    decimal CalculatePrice(int clientId, int boatStandardId, int numOfBoats);
}
