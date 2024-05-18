using System.Data;
using System.Data.SqlClient;
using tutorial07.Exceptions;
using tutorial07.Models;

namespace tutorial07.Services;

public class WarehousesService : IWarehousesService
{
    private readonly IConfiguration _configuration;

    public WarehousesService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> AddProduct(ProductWarehouse productWarehouse)
    {
        int idOrder = await GetOrderIdAsync(productWarehouse);
        double price = await GetProductPriceAsync(productWarehouse);
        await CheckWarehouseExistsAsync(productWarehouse);

        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await UpdateOrderFulfilledAsync(connection, transaction, productWarehouse, idOrder);
                    await InsertProductWarehouseAsync(connection, transaction, productWarehouse, idOrder, price);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return await GetLastInsertedProductWarehouseIdAsync(connection);
        }
    }

    private async Task<int> GetOrderIdAsync(ProductWarehouse productWarehouse)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT TOP 1 [Order].IdOrder FROM [Order] " +
                                      "LEFT JOIN Product_Warehouse ON [Order].IdOrder = Product_Warehouse.IdOrder " +
                                      "WHERE [Order].IdProduct = @IdProduct " +
                                      "AND [Order].Amount = @Amount " +
                                      "AND Product.Warehouse.IdProductWarehouse IS NULL " +
                                      "AND [Order.CreatedAt] < @CreatedAt";
                command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
                command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
                command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

                var idOrder = await command.ExecuteScalarAsync();
                if (idOrder == null) throw new NoCorrespondingOrderException("No corresponding order found!");
                return (int)idOrder;
            }
        }
    }

    private async Task<double> GetProductPriceAsync(ProductWarehouse productWarehouse)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
                command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);

                var price = await command.ExecuteScalarAsync();
                if (price == null) throw new ProductNotFoundException("The product could not be found!");
                return (double)price;
            }
        }
    }

    private async Task CheckWarehouseExistsAsync(ProductWarehouse productWarehouse)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT IdWarehouse FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
                command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);

                var warehouseId = await command.ExecuteScalarAsync();
                if (warehouseId == null) throw new WarehouseNotFoundException("The warehouse could not be found!");
            }
        }
    }

    private async Task UpdateOrderFulfilledAsync(SqlConnection connection, SqlTransaction transaction,
        ProductWarehouse productWarehouse, int idOrder)
    {
        using (var command = connection.CreateCommand())
        {
            command.Transaction = transaction;
            command.CommandText = "UPDATE [Order] SET FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder";
            command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
            command.Parameters.AddWithValue("@IdOrder", idOrder);

            var rowsUpdated = await command.ExecuteNonQueryAsync();
            if (rowsUpdated < 1) throw new OrderAlreadyFulfilledException("Order has been already fulfilled!");
        }
    }

    private async Task InsertProductWarehouseAsync(SqlConnection connection, SqlTransaction transaction,
        ProductWarehouse productWarehouse, int idOrder, double price)
    {
        using (var command = connection.CreateCommand())
        {
            command.Transaction = transaction;
            command.CommandText =
                "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                $"VALUES(@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";
            command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@IdOrder", idOrder);
            command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
            command.Parameters.AddWithValue("@Price", productWarehouse.Amount * price);
            command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

            var rowsInserted = await command.ExecuteNonQueryAsync();
            if (rowsInserted < 1) throw new FailedInsertException("Failed to insert product into warehouse!");
        }
    }

    private async Task<int> GetLastInsertedProductWarehouseIdAsync(SqlConnection connection)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "SELECT TOP 1 IdProductWarehouse FROM Product_Warehouse ORDER BY IdProductWarehouse DESC";

            var idProductWarehouse = await command.ExecuteScalarAsync();
            if (idProductWarehouse == null)
                throw new LastInsertedIdNotFoundException("Failed to get last inserted product warehouse ID!");
            return (int)idProductWarehouse;
        }
    }

    public async Task<int> AddProductWithProc(ProductWarehouse productWarehouse)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "AddProductToWarehouse";
                command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
                command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
                command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
                command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

                var newProductWarehouseId = await command.ExecuteScalarAsync();
                if (newProductWarehouseId == null || newProductWarehouseId == DBNull.Value)
                {
                    throw new NewRecordIdNotAvailableException("Failed to obtain the ID of the newly added record!");
                }

                return Convert.ToInt32(newProductWarehouseId);
            }
        }
    }
}