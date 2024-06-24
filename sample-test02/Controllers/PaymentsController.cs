using Microsoft.AspNetCore.Mvc;
using sample_test02.DTOs;
using sample_test02.Services.Interfaces;

namespace sample_test02.Controllers;

public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
    {
        try
        {
            var paymentId = await _paymentService.AddPaymentAsync(paymentDto.IdClient, paymentDto.IdSubscription, paymentDto.Amount);

            if (paymentId == null)
            {
                return BadRequest("Payment could not be processed!");
            }

            return Ok(paymentId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}