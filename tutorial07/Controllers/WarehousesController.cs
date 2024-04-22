using Microsoft.AspNetCore.Mvc;
using tutorial07.Models;
using tutorial07.Services;

namespace tutorial07.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    private readonly IWarehousesService _warehousesService;

    public WarehousesController(IWarehousesService warehousesService)
    {
        _warehousesService = warehousesService;
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct(ProductWarehouse product)
    {
        int result = await _warehousesService.AddProduct(product);
        return Ok();
    }
}