using sample_test02.DTOs;
using sample_test02.Repositories.Interfaces;
using sample_test02.Services.Interfaces;

namespace sample_test02.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto?> GetClientWithSubscriptionsAsync(int idClient)
    {
        var client = await _clientRepository.GetClientWithSubscriptionsAsync(idClient);

        if (client == null)
        {
            return null;
        }

        var discount = await _clientRepository.GetClientDiscountAsync(idClient);

        var clientDto = new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone,
            Discount = discount > 0 ? $"- {discount}%" : null,
            Subscriptions = client.Sales.Select(s => new SubscriptionDto
            {
                IdSubscription = s.IdSubscription,
                Name = s.IdSubscriptionNavigation.Name,
                RenewalPeriod = s.IdSubscriptionNavigation.RenewalPeriod,
                TotalPaidAmount = s.IdSubscriptionNavigation.Payments.Sum(p => p.Value)
            }).ToList()
        };

        return clientDto;
    }
}