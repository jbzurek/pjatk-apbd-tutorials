using System.Net.WebSockets;
using tutorial03.BaseClasses;
using tutorial03.Exceptions;
using tutorial03.Generators;
using tutorial03.Interfaces;

namespace tutorial03.Containers;

public class GasContainer : Container, IHazardNotifier
{
    public override string ContainerType => "G";
    
    public int Pressure { get; }

    public GasContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight, ContainerSerialNumberGenerator serialNumberGenerator, int pressure)
        : base(netWeight, height, tareWeight, depth, maxWeight, serialNumberGenerator)
    {
        Pressure = pressure;
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
    
    public void Notify(string message, string containerNumber)
    {
        throw new NotImplementedException();
    }
}