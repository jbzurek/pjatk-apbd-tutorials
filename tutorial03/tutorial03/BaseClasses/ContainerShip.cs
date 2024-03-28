using tutorial03.Containers;

namespace tutorial03.BaseClasses;

public class ContainerShip
{
    public static List<ContainerShip> Ships = new List<ContainerShip>();
    public List<Container> Containers { get; private set; }
    public string ShipName { get; set; }
    public int MaxSpeed { get; }
    public int MaxContainers { get; }
    public int MaxWeightCapacity { get; }

    public ContainerShip(string shipName, int maxSpeed, int maxContainers, int maxWeightCapacity)
    {
        ShipName = shipName;
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeightCapacity = maxWeightCapacity;
        Containers = new List<Container>();
        Ships.Add(this);
    }

    public void LoadCargoIntoContainer(Container container, int netWeight)
    {
        container.Load(netWeight);
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count < MaxContainers && CalculateTotalWeight() + container.NetWeight <= MaxWeightCapacity)
        {
            Containers.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} loaded onto the ship.");
        }
        else
        {
            Console.WriteLine(
                $"Could not load container {container.SerialNumber} onto the ship. Ship's capacity exceeded.");
        }
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(string containerNumber)
    {
        var container = Containers.Find(c => c.SerialNumber == containerNumber);
        if (container != null)
        {
            Containers.Remove(container);
            Console.WriteLine($"Container {containerNumber} removed from the ship.");
        }
        else
        {
            Console.WriteLine($"Container {containerNumber} not found on the ship.");
        }
    }

    public void UnloadContainer(Container container)
    {
        if (Containers.Contains(container))
        {
            Containers.Remove(container);
            Console.WriteLine($"Container {container.SerialNumber} unloaded from the ship.");
        }
        else
        {
            Console.WriteLine($"Container {container.SerialNumber} not found on the ship.");
        }
    }

    public void ReplaceContainer(string containerNumber, Container newContainer)
    {
        RemoveContainer(containerNumber);
        LoadContainer(newContainer);
    }

    public void MoveContainer(Container container, ContainerShip destinationShip)
    {
        if (Containers.Contains(container))
        {
            UnloadContainer(container);
            destinationShip.LoadContainer(container);
            Console.WriteLine($"Container {container.SerialNumber} moved to another ship.");
        }
        else
        {
            Console.WriteLine($"Container {container.SerialNumber} not found on this ship.");
        }
    }

    public void PrintContainerInfo(Container container)
    {
        Console.WriteLine($"Container Serial Number: {container.SerialNumber}");
        Console.WriteLine($"Cargo Weight: {container.NetWeight} kg");
        if (container is LiquidContainer liquidContainer)
        {
            Console.WriteLine($"Type: Liquid Container");
        }
        else if (container is GasContainer gasContainer)
        {
            Console.WriteLine($"Type: Gas Container");
        }
        else if (container is CoolContainer coolContainer)
        {
            Console.WriteLine($"Type: Cool Container");
            Console.WriteLine($"Product Type: {coolContainer.ProductType}");
            Console.WriteLine($"Temperature: {coolContainer.RequiredTemperature} °C");
        }
    }

    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship's Max Speed: {MaxSpeed}");
        Console.WriteLine($"Maximum Containers: {MaxContainers}");
        Console.WriteLine($"Max Weight Capacity: {MaxWeightCapacity} tons");
        Console.WriteLine($"Current Number of Containers: {Containers.Count}");
    }

    public void PrintCargoInfo()
    {
        Console.WriteLine("Containers on the ship:");
        foreach (var container in Containers)
        {
            Console.WriteLine(
                $"Container Serial Number: {container.SerialNumber}, Cargo Weight: {container.NetWeight} kg");
        }
    }

    private int CalculateTotalWeight()
    {
        int totalWeight = 0;
        foreach (var container in Containers)
        {
            totalWeight += container.NetWeight;
        }

        return totalWeight;
    }
}