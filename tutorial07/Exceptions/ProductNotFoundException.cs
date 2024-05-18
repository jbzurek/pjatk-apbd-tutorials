namespace tutorial07.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message) : base(message) { }
}
