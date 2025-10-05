// See https://aka.ms/new-console-template for more information
using ConsoleVendingMachine.data.Main;

public class Program
{
    public static void Main()
    {
        Menu menu = new Menu();
        menu.MenuChoice();
    }
}