using sample_test02.Models;

namespace sample_test02.Repositories.Interfaces;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetSubscriptionAsync(int idSubscription);
    Task<Sale?> GetLatestSaleAsync(int idClient, int idSubscription);
}