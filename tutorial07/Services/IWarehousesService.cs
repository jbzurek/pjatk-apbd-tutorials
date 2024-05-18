using tutorial07.Models;

namespace tutorial07.Services;

public interface IWarehousesService
{
    public Task<int> AddProduct(ProductWarehouse productWarehouse);
    public Task<int> AddProductWithProc(ProductWarehouse productWarehouse);
}