using tutorial03.BaseClasses;
using tutorial03.Containers;
using tutorial03.Generators;

namespace tutorial03;

public class Program
{
    public static void Main(string[] args)
    {
        ContainerSerialNumberGenerator containerSerialNumber = new ContainerSerialNumberGenerator();

        ContainerShip ship1 = new ContainerShip("Black Pearl", 30, 20, 1000);
        
        LiquidContainer liquidContainer1 = new LiquidContainer(1, 1, 1, 1, 1, containerSerialNumber, false);
        LiquidContainer liquidContainer2 = new LiquidContainer(1, 1, 1, 1, 1, containerSerialNumber, false);

        CoolContainer coolContainer1 = new CoolContainer(1, 1, 1, 1, 1, containerSerialNumber, "Bananas", 13.3);
        
        
        Menu.DisplayMainMenu();
    }
}