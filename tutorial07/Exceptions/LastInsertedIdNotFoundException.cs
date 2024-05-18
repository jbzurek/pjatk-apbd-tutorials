namespace tutorial07.Exceptions;

public class LastInsertedIdNotFoundException : Exception
{
    public LastInsertedIdNotFoundException(string message) : base(message) { }
}