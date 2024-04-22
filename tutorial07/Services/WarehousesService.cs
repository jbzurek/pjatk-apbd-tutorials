using System.Data.SqlClient;
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
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        using var command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "SELECT TOP 1 [Order].IdOrder FROM [Order] " +
                              "LEFT JOIN Product_Warehouse ON [Order].IdOrder = Product_Warehouse.IdOrder " +
                              "WHERE [Order].IdProduct = @IdProduct " +
                              "AND [Order].Amount = @Amount " +
                              "AND Product.Warehouse.IdProductWarehouse IS NULL " +
                              "AND [Order.CreatedAt] < @CreatedAt";

        command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

        var reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new Exception(); // TODO: Custom Exception

        int idOrder = (int)reader["IdOrder"];

        await reader.CloseAsync();
        command.Parameters.Clear();

        command.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);

        reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new Exception(); // TODO: Custom Exception

        await reader.ReadAsync();

        double price = (double)reader["Price"];

        await reader.CloseAsync();
        command.Parameters.Clear();

        command.CommandText = "SELECT IdWarehouse FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);

        reader = await command.ExecuteReaderAsync();

        if (!reader.HasRows) throw new Exception(); // TODO: Custom Exception

        await reader.CloseAsync();
        command.Parameters.Clear();

        var transaction = (SqlTransaction)await connection.BeginTransactionAsync();
        command.Transaction = transaction;

        try
        {
            command.CommandText = "UPDATE [Order] SET FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder";
            command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);
            command.Parameters.AddWithValue("@IdOrder", idOrder);

            int rowsUpdated = await command.ExecuteNonQueryAsync();
            if (rowsUpdated < 1) throw new Exception(); // TODO: Custom Exception
            command.Parameters.Clear();

            command.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, Amount, Price, CreatedAt) " +
                                  $"VALUES(@IdWarehouse, @IdProduct, @Amount, @Amount * {price}, @CreatedAt)";
            command.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@IdOrder", idOrder);
            command.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
            command.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

            int rowsInserted = await command.ExecuteNonQueryAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new Exception(); // TODO: Custom Exception
        }
        
        command.Parameters.Clear();

        command.CommandText =
            "SELECT TOP 1 IdProductWarehouse FROM Product_Warehouse ORDER BY IdProductWarehouse DESC ";
        reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();
        int idProductWarehouse = (int)reader["IdProductWarehouse"];
        await reader.CloseAsync();
        await connection.CloseAsync();
        
        return idProductWarehouse;
    }
}