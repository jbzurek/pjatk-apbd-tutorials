using tutorial03.Generators;

namespace tutorial03.BaseClasses;

public abstract class Container
{
    public static List<Container> Containers = new List<Container>();
    public int NetWeight { get; protected set; }
    public int Height { get; set; }
    public int TareWeight { get; set; }
    public int Depth { get; set; }
    public int MaxWeight { get; }
    public string SerialNumber { get; }
    public abstract string ContainerType { get; }

    protected Container(int netWeight, int height, int tareWeight, int depth, int maxWeight,
        ContainerSerialNumberGenerator serialNumberGenerator)
    {
        NetWeight = netWeight;
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxWeight = maxWeight;
        SerialNumber = serialNumberGenerator.GenerateSerialNumber(ContainerType);
        Containers.Add(this);
    }

    public abstract void Unload();
    protected abstract void Load(int weight);
    
}