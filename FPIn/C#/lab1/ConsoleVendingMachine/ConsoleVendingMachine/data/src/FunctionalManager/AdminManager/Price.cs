namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class Price
{
    private float price;
    public float priceWrite()
    {
        Console.WriteLine("Введите цену: ");
        price = float.Parse(Console.ReadLine());
        return price;
    }
}