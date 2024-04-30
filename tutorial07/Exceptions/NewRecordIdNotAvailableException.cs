namespace tutorial07.Exceptions;

public class NewRecordIdNotAvailableException : Exception
{
    public NewRecordIdNotAvailableException(string message) : base(message) { }
}