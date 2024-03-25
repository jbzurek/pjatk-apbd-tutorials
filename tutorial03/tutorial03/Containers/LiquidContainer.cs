using tutorial03.BaseClasses;

namespace tutorial03.Containers;

public class LiquidContainer : Container
{
    public LiquidContainer(int netWeight, int height, int tareWeight, int depth, int maxWeight, string serialNumber) : base(netWeight, height, tareWeight, depth, maxWeight, serialNumber)
    {
        
    }

    public override void Unload()
    {
        throw new NotImplementedException();
    }

    public override void Load()
    {
        throw new NotImplementedException();
    }
}