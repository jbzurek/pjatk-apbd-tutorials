using Microsoft.EntityFrameworkCore;
using sample_test02.Context;
using sample_test02.Models;
using sample_test02.Repositories.Interfaces;

namespace sample_test02.Repositories;

public class ClientRepository : IClientRepository
{

    private readonly SampleTest02Context _context;

    public ClientRepository(SampleTest02Context context)
    {
        _context = context;
    }
    
    public async Task<Client?> GetClientAsync(int idClient)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.IdClient == idClient);
    }

    public async Task<Client?> GetClientWithSubscriptionsAsync(int idClient)
    {
        return await _context.Clients
            .Include(c => c.Sales)
            .ThenInclude(s => s.IdSubscriptionNavigation)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);
    }

    public async Task<int?> GetClientDiscountAsync(int idClient)
    {
        return await _context.Discounts
            .Where(d => d.IdClient == idClient && d.DateTo >= DateTime.Now)
            .SumAsync(d => (int?) d.Value) ?? 0;
    }
}