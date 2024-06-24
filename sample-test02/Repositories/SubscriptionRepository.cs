using Microsoft.EntityFrameworkCore;
using sample_test02.Context;
using sample_test02.Models;
using sample_test02.Repositories.Interfaces;

namespace sample_test02.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SampleTest02Context _context;

    public SubscriptionRepository(SampleTest02Context context)
    {
        _context = context;
    }

    public async Task<Subscription?> GetSubscriptionAsync(int idSubscription)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(s => s.IdSubscription == idSubscription);
    }

    public async Task<Sale?> GetLatestSaleAsync(int idClient, int idSubscription)
    {
        return await _context.Sales
            .Where(s => s.IdClient == idClient && s.IdSubscription == idSubscription)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync();
    }
}