using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;

namespace tutorial03.Containers;

public class CoolContainer : Container
{
    public override string ContainerType => "C";

    public static Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>()
    {
        { "bananas", 13.3 },
        { "chocolate", 18 },
        { "fish", 2 },
        { "meat", -15 },
        { "ice cream", -18 },
        { "frozen pizza", -30 },
        { "cheese", 7.2 },
        { "sausages", 5 },
        { "butter", 20.5 },
        { "eggs", 19 }
    };

    public string ProductType { get; }
    public double RequiredTemperature { get; }

    public CoolContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight,
        ContainerSerialNumberGenerator serialNumberGenerator, string productType, double requiredTemperature)
        : base(netWeight, height, tareWeight, depth, maxWeight, serialNumberGenerator)
    {
        ProductType = productType;
        RequiredTemperature = requiredTemperature;
    }

    public override void Unload()
    {
        NetWeight = 0;
    }

    protected override void Load(int weight)
    {
        if (weight + NetWeight > MaxWeight)
        {
            throw new OverfillException("Cargo weight exceeds maximum capacity!");
        }

        NetWeight += weight;
    }
}