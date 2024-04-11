using tutorial03.BaseClasses;

namespace tutorial03;

public class Program
{
    public static void Main(string[] args)
    {
        ContainerShip ship1 = new ContainerShip("Black Pearl", 20, 10, 100000);
        ContainerShip ship2 = new ContainerShip("Santa Maria", 40, 30, 300000);

        Menu menu = new Menu();
        menu.DisplayMainMenu();
    }
}