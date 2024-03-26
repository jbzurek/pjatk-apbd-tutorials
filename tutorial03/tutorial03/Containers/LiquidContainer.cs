using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;
using tutorial03.Interfaces;

namespace tutorial03.Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public override string ContainerType => "L";
    public bool IsDangerous { get; }
    
    public LiquidContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight, ContainerSerialNumberGenerator serialNumberGenerator, bool isDangerous)
        : base(netWeight, height, tareWeight, depth, maxWeight, serialNumberGenerator)
    {
        IsDangerous = isDangerous;
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

        NetWeight += weight;
    }

    public void Notify(string message, string containerNumber)
    {
        Console.Write($"Hazard notification for container {containerNumber}: {message}");
    }
}