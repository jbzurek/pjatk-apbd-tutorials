using Microsoft.AspNetCore.Mvc;
using sample_test02.Services.Interfaces;

namespace sample_test02.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{idClient}")]
    public async Task<IActionResult> GetClientWithSubscriptions(int idClient)
    {
        var client = await _clientService.GetClientWithSubscriptionsAsync(idClient);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }
}