namespace tutorial03.Exceptions;

public class OverfillException : Exception
{
    public OverfillException(string msg) : base(msg)
    {
        throw new Exception(msg);
    }
}