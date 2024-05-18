namespace tutorial07.Exceptions;

public class NoCorrespondingOrderException : Exception
{
    public NoCorrespondingOrderException(string message) : base(message) { }
}
