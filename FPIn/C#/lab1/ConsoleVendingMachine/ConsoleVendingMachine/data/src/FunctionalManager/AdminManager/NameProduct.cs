namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class NameProduct
{
    public void Print(string name)
    {
        Console.WriteLine("Введите название продукта: ");
        name = Console.ReadLine();

        if (name == null)
        {
            Console.WriteLine("Имя не должно быть пустым!");
            Print(name);
            return;
        }
    }
}

