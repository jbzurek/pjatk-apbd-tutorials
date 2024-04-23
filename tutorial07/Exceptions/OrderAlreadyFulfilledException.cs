namespace tutorial07.Exceptions;

public class OrderAlreadyFulfilledException : Exception
{
    public OrderAlreadyFulfilledException(string message) : base(message) { }
}
