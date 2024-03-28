using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;

namespace tutorial03.Containers;

public class CoolContainer : Container
{
    public override string ContainerType => "C";
    private ContainerSerialNumberGenerator generator = new ContainerSerialNumberGenerator();

    public static Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>()
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
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

    public override void Load(int weight)
    {
        if (weight + NetWeight > MaxWeight)
        {
            throw new OverfillException("Cargo weight exceeds maximum capacity!");
        }
    }

    public void CreateCoolContainer()
    {
        Console.WriteLine("Enter values");

        Console.Write("net weight: ");
        int netWeight = int.Parse(Console.ReadLine());

        Console.Write("height: ");
        int height = int.Parse(Console.ReadLine());

        Console.Write("max cargo weight: ");
        int tareWeight = int.Parse(Console.ReadLine());

        Console.Write("depth: ");
        int depth = int.Parse(Console.ReadLine());

        Console.Write("max weight: ");
        int maxWeight = int.Parse(Console.ReadLine());

        Console.Write("product type: ");
        string productType = Console.ReadLine();
        if (!ProductTemperatures.ContainsKey(productType))
        {
            Console.Write("Invalid product type! Try again.");
            CreateCoolContainer();
        }

        double productTemperature = ProductTemperatures[productType];

        CoolContainer container = new CoolContainer(netWeight, height, tareWeight, depth, maxWeight, generator,
            productType, productTemperature);
        Containers.Add(container);
    }
}