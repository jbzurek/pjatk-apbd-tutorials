namespace tutorial03.BaseClasses;

public class ContainerShip
{
    public static List<ContainerShip> Ships = new List<ContainerShip>();
    public List<Container> Containers { get; }
    public string ShipName { get; }
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

    public static void CreateNewShip()
    {
        Console.Clear();
        Console.WriteLine("\nEnter values");

        Console.Write("ship's name: ");
        string shipName = Console.ReadLine();

        Console.Write("max speed: ");
        int maxSpeed = int.Parse(Console.ReadLine());

        Console.Write("max number of containers: ");
        int maxNumContainer = int.Parse(Console.ReadLine());

        Console.Write("max weight: ");
        int maxWeight = int.Parse(Console.ReadLine());

        ContainerShip ship = new ContainerShip(shipName, maxSpeed, maxNumContainer, maxWeight);
        Console.WriteLine("Press enter to return");
    }

    public void ReplaceContainer(string containerNumber, Container newContainer)
    {
        var containerToRemove = Containers.Find(c => c.SerialNumber == containerNumber);
        if (containerToRemove != null)
        {
            Containers.Remove(containerToRemove);
            Containers.Add(newContainer);
            Console.WriteLine(
                $"Container {containerNumber} replaced with container {newContainer.SerialNumber} on the ship.");
        }
        else
        {
            Console.WriteLine($"Container {containerNumber} not found on the ship.");
        }
    }

    public int CalculateTotalWeight()
    {
        int totalWeight = 0;
        foreach (var container in Containers)
        {
            totalWeight += container.NetWeight;
        }

        return totalWeight;
    }
}