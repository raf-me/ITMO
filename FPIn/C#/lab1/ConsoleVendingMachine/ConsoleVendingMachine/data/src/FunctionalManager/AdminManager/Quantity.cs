namespace ConsoleVendingMachine.data.FunctionalManager.AssortmentManager;

public class Quantity
{
    public void Check(int quantity)
    {
        Console.WriteLine("Введите количество: ");
        quantity = int.Parse(Console.ReadLine());
    }
}