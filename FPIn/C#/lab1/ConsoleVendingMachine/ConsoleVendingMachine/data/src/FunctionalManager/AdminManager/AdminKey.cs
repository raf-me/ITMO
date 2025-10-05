using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;

namespace ConsoleVendingMachine.data.FunctionalManager.AdminManager;

public class AdminKey
{
    public void AdminWork()
    {
        var dataAdmin = new DataAdmin();
        
        Console.WriteLine("Введите имя: ");
        string nameWrite = Console.ReadLine();
        
        Console.WriteLine("Введите пароль: ");
        int keyWrite = int.Parse(Console.ReadLine());

        if (dataAdmin.Admins.Any(a => a.Name == nameWrite && a.Key == keyWrite))
        {
            AssortmentAdmin assortment = new AssortmentAdmin();
            Console.WriteLine("Выберите действие (Add или Remove): ");
            string answer = Console.ReadLine();

            if (answer != "Add" && answer != "Remove") throw new ArgumentException();
            
            if (answer == "Add") { assortment.AddManager(); }

            if (answer == "Remove") { assortment.RemoveManager(); }
        }

        else
        {
            Console.WriteLine("Неверное имя или пароль!");
        }
    }
}