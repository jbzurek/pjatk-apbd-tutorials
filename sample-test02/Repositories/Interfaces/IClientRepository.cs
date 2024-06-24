using sample_test02.Models;

namespace sample_test02.Repositories.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetClientAsync(int idClient);
    Task<Client?> GetClientWithSubscriptionsAsync(int idClient);
    Task<int?> GetClientDiscountAsync(int idClient);
}