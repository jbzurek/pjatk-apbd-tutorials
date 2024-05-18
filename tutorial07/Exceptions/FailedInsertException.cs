namespace tutorial07.Exceptions;

public class FailedInsertException : Exception
{
    public FailedInsertException(string message) : base(message) { }
}