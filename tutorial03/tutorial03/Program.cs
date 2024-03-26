using tutorial03.BaseClasses;

namespace tutorial03;

public class Program
{
    static List<ContainerShip> containerShips = new List<ContainerShip>();

    static void Main(string[] args)
    {
        DisplayMainMenu();
    }

    static void DisplayMainMenu()
    {
        Console.WriteLine("Lista kontenerowców:");
        if (containerShips.Count == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            foreach (var ship in containerShips)
            {
                Console.WriteLine($"(speed={ship.MaxSpeed}, maxContainerNum={ship.MaxContainers}, maxWeight={ship.MaxWeightCapacity})");
            }
        }

        Console.WriteLine("\nLista kontenerów:");
        Console.WriteLine("Brak");

        Console.WriteLine("\nMożliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usuń kontenerowiec");
        Console.WriteLine("3. Dodaj kontener");
        Console.WriteLine("4. Usuń kontener");
        Console.WriteLine("5. Wyświetl informacje o kontenerach na statku");
        Console.WriteLine("6. Wyświetl informacje o kontenerze");
        Console.WriteLine("0. Wyjdź");

        Console.Write("\nWybierz akcję: ");
        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowy wybór.");
        }

        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
        Console.Clear();
        DisplayMainMenu();
    }
    
    
    
}