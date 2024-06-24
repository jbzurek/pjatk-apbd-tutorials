using sample_test02.DTOs;

namespace sample_test02.Services.Interfaces;

public interface IClientService
{
    Task<ClientDto?> GetClientWithSubscriptionsAsync(int idClient);
}