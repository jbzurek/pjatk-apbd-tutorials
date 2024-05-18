namespace tutorial07.Exceptions;

public class WarehouseNotFoundException : Exception
{
    public WarehouseNotFoundException(string message) : base(message) { }
}
