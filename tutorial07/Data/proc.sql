CREATE PROCEDURE AddProductToWarehouse
    @IdProduct INT, 
    @IdWarehouse INT, 
    @Amount INT, 
    @CreatedAt DATETIME
AS
BEGIN  
   
    DECLARE @IdProductFromDb INT, @IdOrder INT, @Price DECIMAL(5,2);

    SELECT TOP 1 @IdOrder = o.IdOrder, @Price = p.Price
    FROM [Order] o
    LEFT JOIN Product p ON o.IdProduct = p.IdProduct
    WHERE o.IdProduct = @IdProduct
    AND o.Amount = @Amount
    AND o.FulfilledAt IS NULL
    AND o.CreatedAt < @CreatedAt;

    IF @IdOrder IS NULL
    BEGIN  
        RAISERROR('Invalid parameter: There is no order to fulfill', 18, 0);  
        RETURN;
    END;  
    
    IF NOT EXISTS(SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse)
    BEGIN  
        RAISERROR('Invalid parameter: Provided IdWarehouse does not exist', 18, 0);  
        RETURN;
    END;  
    
    IF @Price IS NULL
    BEGIN  
        RAISERROR('Invalid parameter: Provided IdProduct does not exist', 18, 0);  
        RETURN;
    END;  

    SET XACT_ABORT ON;
    BEGIN TRAN;

    UPDATE [Order]
    SET FulfilledAt = @CreatedAt
    WHERE IdOrder = @IdOrder;

    INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
    VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Amount * @Price, @CreatedAt);

    SELECT @@IDENTITY AS NewId;

    COMMIT;

END;