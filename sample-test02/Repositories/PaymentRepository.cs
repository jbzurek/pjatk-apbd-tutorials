using Microsoft.EntityFrameworkCore;
using sample_test02.Context;
using sample_test02.Models;
using sample_test02.Repositories.Interfaces;

namespace sample_test02.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly SampleTest02Context _context;

    public PaymentRepository(SampleTest02Context context)
    {
        _context = context;
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<Payment?> GetExistingPaymentAsync(int idClient, int idSubscription, DateTime startDate,
        DateTime endDate)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(p =>
                p.IdClient == idClient && p.IdSubscription == idSubscription && p.Date >= startDate &&
                p.Date < endDate);
    }
}