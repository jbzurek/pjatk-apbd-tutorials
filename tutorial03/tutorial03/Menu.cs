using tutorial03.BaseClasses;
using tutorial03.Containers;
using tutorial03.Generators;

namespace tutorial03;

public class Menu
{
    ContainerSerialNumberGenerator generator = new ContainerSerialNumberGenerator();

    public void DisplayMainMenu()
    {
        while (true)
        {
            PrintListOfShips();
            PrintListOfContainers();

            Console.WriteLine("\nPossible actions:");
            Console.WriteLine("Enter (1) to create new container\n" +
                              "Enter (2) to create new ship\n\n" +
                              
                              "Enter (3) to print info about specific container\n" +
                              "Enter (4) to print info about specific ship\n\n" +
                              
                              "Enter (5) to load container onto the ship\n" +
                              "Enter (6) to load list of containers onto the ship\n\n" +
                              
                              "Enter (7) to remove container from ship\n" +
                              "Enter (8) to unload container\n\n" +
                              
                              "Enter (9) to move container between ships\n" +
                              "Enter (10) to replace container on ship\n\n" +
                              
                              "Enter (0) to exit\n");

            Console.Write("\nYour action: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ChooseContainer();
                        break;
                    case 2:
                        ContainerShip.CreateNewShip();
                        break;
                    case 3:
                        PrintContainerInfo();
                        break;
                    case 4:
                        PrintShipInfo();
                        break;
                    case 5:
                        LoadContainer();
                        break;
                    case 6:
                        LoadContainers();
                        break;
                    case 7:
                        RemoveContainerFromShip();
                        break;
                    case 8:
                        UnloadCargoFromContainer();
                        break;
                    case 9:
                        MoveContainerBetweenShips();
                        break;
                    case 10:
                        ReplaceContainerOnShip();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void PrintListOfContainers()
    {
        Console.WriteLine("\nList of containers:");
        if (Container.Containers.Count == 0)
        {
            Console.WriteLine("-");
        }
        else
        {
            foreach (var container in Container.Containers)
            {
                Console.WriteLine(
                    $"serial number: {container.SerialNumber}, type: {container.ContainerType}, net weight: {container.NetWeight}");
            }
        }
    }

    private void PrintListOfShips()
    {
        Console.WriteLine("\nList of ships:");
        if (ContainerShip.Ships.Count == 0)
        {
            Console.WriteLine("-");
        }
        else
        {
            foreach (var ship in ContainerShip.Ships)
            {
                Console.WriteLine(
                    $"name: {ship.ShipName}, max containers: {ship.MaxContainers}");
            }
        }
    }

    private void ChooseContainer()
    {
        Console.Clear();
        Console.WriteLine("\nEnter (1) to create Cool Container\n" +
                          "Enter (2) to create Gas Container\n" +
                          "Enter (3) to create Liquid Container\n" +
                          "Enter (0) to return\n");
        Console.Write("Your action: ");
        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    CreateCoolContainer();
                    break;
                case 2:
                    CreateGasContainer();
                    break;
                case 3:
                    CreateLiquidContainer();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
    }

    private void CreateCoolContainer()
    {
        Console.WriteLine("\nEnter values");

        Console.Write("max cargo weight: ");
        int netWeight = int.Parse(Console.ReadLine());

        Console.Write("height: ");
        int height = int.Parse(Console.ReadLine());

        Console.Write("container's weight: ");
        int tareWeight = int.Parse(Console.ReadLine());

        Console.Write("depth: ");
        int depth = int.Parse(Console.ReadLine());

        Console.Write("max total weight: ");
        int maxWeight = int.Parse(Console.ReadLine());

        Console.Write("product type: ");
        string productType = Console.ReadLine();
        if (!CoolContainer.ProductTemperatures.ContainsKey(productType))
        {
            Console.Clear();
            Console.WriteLine("Invalid product type! Try again.");
            CreateCoolContainer();
        }

        double productTemperature = CoolContainer.ProductTemperatures[productType];

        CoolContainer container = new CoolContainer(netWeight, height, tareWeight, depth, maxWeight, generator,
            productType, productTemperature);
    }

    private void CreateGasContainer()
    {
        Console.WriteLine("\nEnter values");

        Console.Write("max cargo weight: ");
        int netWeight = int.Parse(Console.ReadLine());

        Console.Write("height: ");
        int height = int.Parse(Console.ReadLine());

        Console.Write("container's weight: ");
        int tareWeight = int.Parse(Console.ReadLine());

        Console.Write("depth: ");
        int depth = int.Parse(Console.ReadLine());

        Console.Write("max total weight: ");
        int maxWeight = int.Parse(Console.ReadLine());

        Console.Write("pressure: ");
        int pressure = int.Parse(Console.ReadLine());

        GasContainer container = new GasContainer(netWeight, height, tareWeight, depth, maxWeight, generator, pressure);

        container.Notify("BE careful!", container.SerialNumber);
    }

    private void CreateLiquidContainer()
    {
        Console.WriteLine("\nEnter values");

        Console.Write("max cargo weight: ");
        int netWeight = int.Parse(Console.ReadLine());

        Console.Write("height: ");
        int height = int.Parse(Console.ReadLine());

        Console.Write("container's weight: ");
        int tareWeight = int.Parse(Console.ReadLine());

        Console.Write("depth: ");
        int depth = int.Parse(Console.ReadLine());

        Console.Write("max total weight: ");
        int maxWeight = int.Parse(Console.ReadLine());

        Console.Write("is cargo dangerous? (1) yes / (0) no: ");
        int dangerous = int.Parse(Console.ReadLine());
        bool isDangerous = false;
        if (dangerous == 1)
        {
            isDangerous = true;
        }
        else if (dangerous == 0)
        {
            isDangerous = false;
        }

        LiquidContainer container =
            new LiquidContainer(netWeight, height, tareWeight, depth, maxWeight, generator, isDangerous);

        if (isDangerous)
        {
            container.Notify("Container can be dangerous!", container.SerialNumber);
        }
    }

    private void PrintContainerInfo()
    {
        Console.WriteLine("Choose container: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choice = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        Console.WriteLine($"Container Serial Number: {Container.Containers[choice].SerialNumber}");
        Console.WriteLine($"Cargo Weight: {Container.Containers[choice].NetWeight} kg");
        Console.WriteLine($"Depth: {Container.Containers[choice].Depth}");
        Console.WriteLine($"Max weight: {Container.Containers[choice].MaxWeight} kg");
        Console.WriteLine($"Empty container's weight: {Container.Containers[choice].TareWeight} kg");
        if (Container.Containers[choice] is LiquidContainer)
        {
            Console.WriteLine("Type: Liquid Container");
        }
        else if (Container.Containers[choice] is GasContainer)
        {
            Console.WriteLine("Type: Gas Container");
        }
        else if (Container.Containers[choice] is CoolContainer coolContainer)
        {
            Console.WriteLine("Type: Cool Container");
            Console.WriteLine($"Product Type: {coolContainer.ProductType}");
            Console.WriteLine($"Temperature: {coolContainer.RequiredTemperature} °C");
        }

        Console.WriteLine("Press enter to return");
    }

    private void PrintShipInfo()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Choose ship: ");
        for (int i = 0; i < ContainerShip.Ships.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + ContainerShip.Ships[i].ShipName);
        }

        Console.Write("Your action: ");
        int choice = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        Console.WriteLine($"Ship's Max Speed: {ContainerShip.Ships[choice].MaxSpeed}");
        Console.WriteLine($"Maximum Containers: {ContainerShip.Ships[choice].MaxContainers}");
        Console.WriteLine($"Max Weight Capacity: {ContainerShip.Ships[choice].MaxWeightCapacity} kg");
        Console.WriteLine("Current containers: ");
        foreach (var container in ContainerShip.Ships[choice].Containers)
        {
            Console.Write(container.SerialNumber + ", ");
        }

        Console.WriteLine();
        Console.WriteLine("Press enter to return");
    }

    private void LoadContainer()
    {
        Console.Clear();
        Console.WriteLine("Choose container: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choiceContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        Console.WriteLine("Choose ship: ");
        for (int i = 0; i < ContainerShip.Ships.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + ContainerShip.Ships[i].ShipName);
        }

        Console.Write("Your action: ");
        int choiceShip = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        if (ContainerShip.Ships[choiceShip].Containers.Count < ContainerShip.Ships[choiceShip].MaxContainers &&
            ContainerShip.Ships[choiceShip].CalculateTotalWeight() + Container.Containers[choiceContainer].NetWeight <=
            ContainerShip.Ships[choiceShip].MaxWeightCapacity)
        {
            ContainerShip.Ships[choiceShip].Containers.Add(Container.Containers[choiceContainer]);
            Console.WriteLine($"Container {Container.Containers[choiceContainer].SerialNumber} loaded onto the ship.");
        }
        else
        {
            Console.WriteLine(
                $"Could not load container {Container.Containers[choiceContainer].SerialNumber} onto the ship. Ship's capacity exceeded.");
        }
    }

    private void LoadContainers()
    {
        Console.Clear();
        Console.WriteLine("Available containers: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.WriteLine("Choose containers to load (comma-separated, e.g., '0,1,2'): ");
        string input = Console.ReadLine();
        string[] choices = input.Split(',');

        Console.WriteLine("\nChoose ship: ");
        for (int i = 0; i < ContainerShip.Ships.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + ContainerShip.Ships[i].ShipName);
        }

        Console.Write("Your action: ");
        int choiceShip = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        foreach (string choice in choices)
        {
            int index;
            if (int.TryParse(choice, out index) && index >= 0 && index < Container.Containers.Count)
            {
                if (ContainerShip.Ships[choiceShip].Containers.Count < ContainerShip.Ships[choiceShip].MaxContainers &&
                    ContainerShip.Ships[choiceShip].CalculateTotalWeight() + Container.Containers[index].NetWeight <=
                    ContainerShip.Ships[choiceShip].MaxWeightCapacity)
                {
                    ContainerShip.Ships[choiceShip].Containers.Add(Container.Containers[index]);
                    Console.WriteLine(
                        $"Container {Container.Containers[index].SerialNumber} loaded onto the ship {ContainerShip.Ships[choiceShip].ShipName}.");
                }
                else
                {
                    Console.WriteLine(
                        $"Could not load container {Container.Containers[index].SerialNumber} onto the ship {ContainerShip.Ships[choiceShip].ShipName}. Ship's capacity exceeded.");
                }
            }
            else
            {
                Console.WriteLine($"Invalid choice: {choice}");
            }
        }
    }

    private void RemoveContainerFromShip()
    {
        Console.Clear();
        Console.WriteLine("Choose ship: ");
        for (int i = 0; i < ContainerShip.Ships.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + ContainerShip.Ships[i].ShipName);
        }

        Console.Write("Your action: ");
        int choiceShip = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        if (ContainerShip.Ships[choiceShip].Containers.Count == 0)
        {
            Console.WriteLine($"Ship {ContainerShip.Ships[choiceShip].ShipName} has no containers loaded.");
            return;
        }

        Console.WriteLine($"Containers on ship {ContainerShip.Ships[choiceShip].ShipName}: ");
        for (int i = 0; i < ContainerShip.Ships[choiceShip].Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + ContainerShip.Ships[choiceShip].Containers[i].SerialNumber);
        }

        Console.Write("Choose container to remove: ");
        int choiceContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();


        if (choiceContainer >= 0 && choiceContainer < ContainerShip.Ships[choiceShip].Containers.Count)
        {
            Container removedContainer = ContainerShip.Ships[choiceShip].Containers[choiceContainer];
            ContainerShip.Ships[choiceShip].Containers.RemoveAt(choiceContainer);
            Console.WriteLine(
                $"Container {removedContainer.SerialNumber} removed from ship {ContainerShip.Ships[choiceShip].ShipName}.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private void UnloadCargoFromContainer()
    {
        Console.Clear();
        Console.WriteLine("Choose container to unload cargo from: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choiceContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        bool containerLoaded = false;
        foreach (var ship in ContainerShip.Ships)
        {
            if (ship.Containers.Contains(Container.Containers[choiceContainer]))
            {
                containerLoaded = true;
                break;
            }
        }

        if (!containerLoaded)
        {
            Console.WriteLine(
                $"Container {Container.Containers[choiceContainer].SerialNumber} is not currently loaded on any ship.");
            return;
        }

        Container.Containers[choiceContainer].Unload();
        Console.WriteLine($"Cargo unloaded from container {Container.Containers[choiceContainer].SerialNumber}.");
    }

    private void MoveContainerBetweenShips()
    {
        Console.Clear();
        Console.WriteLine("Choose container to move: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choiceContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        bool containerLoaded = false;
        ContainerShip currentShip = null;
        foreach (var ship in ContainerShip.Ships)
        {
            if (ship.Containers.Contains(Container.Containers[choiceContainer]))
            {
                containerLoaded = true;
                currentShip = ship;
                break;
            }
        }

        if (!containerLoaded)
        {
            Console.WriteLine(
                $"Container {Container.Containers[choiceContainer].SerialNumber} is not currently loaded on any ship.");
            return;
        }

        Console.WriteLine("Choose destination ship: ");
        for (int i = 0; i < ContainerShip.Ships.Count; i++)
        {
            if (ContainerShip.Ships[i] != currentShip)
            {
                Console.WriteLine("(" + i + ") " + ContainerShip.Ships[i].ShipName);
            }
        }

        Console.Write("Your action: ");
        int choiceDestinationShip = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        if (ContainerShip.Ships[choiceDestinationShip].Containers.Count >=
            ContainerShip.Ships[choiceDestinationShip].MaxContainers)
        {
            Console.WriteLine(
                $"Destination ship {ContainerShip.Ships[choiceDestinationShip].ShipName} cannot accommodate more containers.");
            return;
        }

        Container containerToMove = Container.Containers[choiceContainer];
        currentShip.Containers.Remove(containerToMove);
        ContainerShip.Ships[choiceDestinationShip].Containers.Add(containerToMove);

        Console.WriteLine(
            $"Container {containerToMove.SerialNumber} moved from ship {currentShip.ShipName} to ship {ContainerShip.Ships[choiceDestinationShip].ShipName}.");
    }

    private void ReplaceContainerOnShip()
    {
        Console.Clear();
        Console.WriteLine("Choose container to replace: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choiceContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        bool containerLoaded = false;
        Container containerToReplace = Container.Containers[choiceContainer];
        ContainerShip currentShip = null;
        foreach (var ship in ContainerShip.Ships)
        {
            if (ship.Containers.Contains(containerToReplace))
            {
                containerLoaded = true;
                currentShip = ship;
                break;
            }
        }

        if (!containerLoaded)
        {
            Console.WriteLine($"Container {containerToReplace.SerialNumber} is not currently loaded on any ship.");
            return;
        }

        Console.WriteLine("Choose container to replace with: ");
        for (int i = 0; i < Container.Containers.Count; i++)
        {
            Console.WriteLine("(" + i + ") " + Container.Containers[i].SerialNumber);
        }

        Console.Write("Your action: ");
        int choiceNewContainer = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine();

        currentShip.ReplaceContainer(containerToReplace.SerialNumber, Container.Containers[choiceNewContainer]);
    }
}