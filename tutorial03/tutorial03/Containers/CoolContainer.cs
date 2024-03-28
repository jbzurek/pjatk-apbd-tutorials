using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;

namespace tutorial03.Containers;

public class CoolContainer : Container
{
    public override string ContainerType => "C";

    private static Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>()
    {
        {"Bananas", 13.3},
        {"Chocolate", 18},
        {"Fish", 2},
        {"Meat", -15},
        {"Ice cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };

    public string ProductType { get; }
    public double RequiredTemperature { get; }
    
    public CoolContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight, ContainerSerialNumberGenerator serialNumberGenerator, string productType, double requiredTemperature)
        : base(netWeight, height, tareWeight, depth, maxWeight, serialNumberGenerator)
    {
        ProductType = productType;
        RequiredTemperature = requiredTemperature;
    }
    
    public override void Unload()
    {
        NetWeight = 0;
    }

    public override void Load(int weight)
    {
        if (weight + NetWeight > MaxWeight)
        {
            throw new OverfillException("Cargo weight exceeds maximum capacity!");
        }
    }

    public bool IsTemperatureValid()
    {
        if (!ProductTemperatures.ContainsKey(ProductType))
        {
            throw new Exception("Invalid product type!");
        }

        return ProductTemperatures[ProductType] <= RequiredTemperature;
    }
}