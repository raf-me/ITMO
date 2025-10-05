namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class Price
{
    public void priceWrite(float price)
    {
        Console.WriteLine("Введите цену: ");
        price = float.Parse(Console.ReadLine());
    }
}