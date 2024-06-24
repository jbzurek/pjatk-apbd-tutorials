using Microsoft.EntityFrameworkCore;
using sample_test02.Context;
using sample_test02.Repositories.Interfaces;
using sample_test02.Services.Interfaces;
using sample_test02.Models;

namespace sample_test02.Services;

public class PaymentService : IPaymentService
{
    private readonly IClientRepository _clientRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly SampleTest02Context _context;

    public PaymentService(IClientRepository clientRepository, ISubscriptionRepository subscriptionRepository,
        IPaymentRepository paymentRepository, SampleTest02Context context)
    {
        _clientRepository = clientRepository;
        _subscriptionRepository = subscriptionRepository;
        _paymentRepository = paymentRepository;
        _context = context;
    }

    public async Task<int?> AddPaymentAsync(int idClient, int idSubscription, decimal paymentAmount)
    {
        var client = await _clientRepository.GetClientAsync(idClient);
        if (client == null)
        {
            return null;
        }

        var subscription = await _subscriptionRepository.GetSubscriptionAsync(idSubscription);
        if (subscription == null)
        {
            return null;
        }

        if (subscription.EndTime < DateTime.Now)
        {
            return null;
        }

        var latestSale = await _subscriptionRepository.GetLatestSaleAsync(idClient, idSubscription);
        if (latestSale == null)
        {
            return null;
        }

        var existingPayment = await _paymentRepository.GetExistingPaymentAsync(idClient, idSubscription,
            latestSale.CreatedAt, latestSale.CreatedAt.AddMonths(subscription.RenewalPeriod));
        if (existingPayment != null)
        {
            return null;
        }

        var discounts = await _context.Discounts
            .Where(d => d.IdClient == idClient && d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now)
            .ToListAsync();

        var totalDiscount = discounts.Sum(d => d.Value);
        if (totalDiscount > 50)
        {
            totalDiscount = 50;
        }

        var expectedAmount = subscription.Price * (1 - (totalDiscount / 100m));
        if (paymentAmount != expectedAmount)
        {
            return null;
        }

        var payment = new Payment
        {
            IdClient = idClient,
            IdSubscription = idSubscription,
            Value = paymentAmount,
            Date = DateTime.Now
        };

        await _paymentRepository.AddPaymentAsync(payment);
        return payment.IdPayment;
    }
}