using tutorial03.BaseClasses;

namespace tutorial03;

public class Menu
{
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
}