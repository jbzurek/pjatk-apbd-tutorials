namespace sample_test02.Services.Interfaces;

public interface IPaymentService
{
    Task<int?> AddPaymentAsync(int idClient, int idSubscription, decimal paymentAmount);
}