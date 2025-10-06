namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class NameProduct
{
    private string name;
    
    public string Print()
    {
        Console.WriteLine("Введите название продукта: ");
        name = Console.ReadLine();

        if (name == null)
        {
            Console.WriteLine("Имя не должно быть пустым!");
            return Print();
        }
        return name;
    }
}

