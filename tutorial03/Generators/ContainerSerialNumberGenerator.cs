namespace tutorial03.Generators;

public class ContainerSerialNumberGenerator
{
    private static Dictionary<string, int> _serialNumber;

    public ContainerSerialNumberGenerator()
    {
        _serialNumber = new Dictionary<string, int>();
    }

    public string GenerateSerialNumber(string containerType)
    {
        string serialNumber = $"KON-{containerType}-";

        if (!_serialNumber.ContainsKey(containerType))
        {
            _serialNumber[containerType] = 1;
        }
        else
        {
            _serialNumber[containerType]++;
        }

        serialNumber += _serialNumber[containerType].ToString();
        return serialNumber;
    }
}