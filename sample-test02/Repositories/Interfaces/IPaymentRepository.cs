using sample_test02.Models;

namespace sample_test02.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task AddPaymentAsync(Payment payment);
    Task<Payment?> GetExistingPaymentAsync(int idClient, int idSubscription, DateTime startDate, DateTime endDate);
}