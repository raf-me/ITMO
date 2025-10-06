namespace ConsoleVendingMachine.data.FunctionalManager.AssortmentManager;

public class Quantity
{
    private int quantity;
    public int Check()
    {
        Console.WriteLine("Введите количество: ");
        quantity = int.Parse(Console.ReadLine());
        return quantity;
    }
}