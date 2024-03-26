using tutorial03.Generators;

namespace tutorial03.BaseClasses;

public abstract class Container
{
    public int NetWeight { get; set; }
    public int Height { get; set; }
    public int TareWeight { get; set; }
    public int Depth { get; set; }
    public int MaxWeight { get; set; }
    public string SerialNumber { get; set; }
    public abstract string ContainerType { get; }

    public Container(int netWeight, int height, int tareWeight, int depth, int maxWeight, ContainerSerialNumberGenerator serialNumberGenerator)
    {
        NetWeight = netWeight;
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxWeight = maxWeight;
        SerialNumber = serialNumberGenerator.GenerateSerialNumber(ContainerType);
    }

    public abstract void Unload();
    public abstract void Load();
    
}