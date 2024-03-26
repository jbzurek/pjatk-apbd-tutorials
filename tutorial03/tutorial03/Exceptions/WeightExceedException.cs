namespace tutorial03.Exceptions;

public class WeightExceedException : Exception
{
    public WeightExceedException(string message) : base(message)
    {
        throw new Exception(message);
    }
}