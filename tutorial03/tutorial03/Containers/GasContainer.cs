using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;
using tutorial03.Interfaces;

namespace tutorial03.Containers;

public class GasContainer : Container, IHazardNotifier
{
    public override string ContainerType => "G";

    public int Pressure { get; set; }

    public GasContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight,
        ContainerSerialNumberGenerator serialNumberGenerator, int pressure)
        : base(netWeight, height, tareWeight, depth, maxWeight, serialNumberGenerator)
    {
        Pressure = pressure;
    }

    public override void Unload()
    {
        NetWeight = (int)(NetWeight * 0.05);
    }

    protected override void Load(int weight)
    {
        if (weight + NetWeight > MaxWeight)
        {
            throw new OverfillException("Cargo weight exceeds maximum capacity!");
        }

        NetWeight += weight;
    }

    public void Notify(string message, string containerNumber)
    {
        Console.Write($"Hazard notification for container {containerNumber}: {message}");
    }
}