using tutorial03.BaseClasses;
using tutorial03.Containers;
using tutorial03.Generators;

namespace tutorial03;

public class Menu
{
    private static ContainerSerialNumberGenerator generator = new ContainerSerialNumberGenerator();

    public static void DisplayMainMenu()
    {
        while (true)
        {
            PrintListOfShips();
            PrintListOfContainers();

            Console.WriteLine("\nPossible actions:");
            Console.WriteLine("1. Create new container");
            Console.WriteLine("0. Exit");

            Console.Write("\nYour action: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ChooseContainer();
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

    private static void PrintListOfContainers()
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

    private static void PrintListOfShips()
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

    private static void ChooseContainer()
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
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
    }

    public static void CreateCoolContainer()
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
        if (!CoolContainer.ProductTemperatures.ContainsKey(productType))
        {
            Console.Write("Invalid product type! Try again.");
            CreateCoolContainer();
        }

        double productTemperature = CoolContainer.ProductTemperatures[productType];

        CoolContainer container = new CoolContainer(netWeight, height, tareWeight, depth, maxWeight, generator,
            productType, productTemperature);
        Container.Containers.Add(container);
    }
}