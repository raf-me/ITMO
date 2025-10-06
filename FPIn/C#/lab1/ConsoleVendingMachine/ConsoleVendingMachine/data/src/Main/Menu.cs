using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;
using ConsoleVendingMachine.data.FunctionalManager.AdminManager;
using ConsoleVendingMachine.data.FunctionalManager.MoneyAndPayManager;
using ConsoleVendingMachine.data.InterfaceManager;

namespace ConsoleVendingMachine.data.Main;

public class Menu
{
    public void MenuChoice()
    {
        Console.WriteLine("Добрый день! Добро пожаловать в автомат Томспона! \n" +
                          "Введите menu, чтобы увидеть каталог доступных функций.");
        string answer = Console.ReadLine();

        if (answer == "exit")
        {
            Environment.Exit(0);
        }

        if (answer == "menu")
        {
            Console.WriteLine("Вы хотите совершить покупку?");
            answer = Console.ReadLine();
            if (answer == "Admin")
            {
                AdminKey adminKey = new AdminKey();
                adminKey.AdminWork();
                return;
            }

            if (answer == "Да" || answer == "да")
            {   
                DataCollection dc = new DataCollection();
                dc.PrintAllProducts();
                
                MoneyManager moneyManager = new MoneyManager();
                moneyManager.Manager();
                return;
            }
        }

        if (answer == "IvanZolo")
        {
            Console.WriteLine("Вы разблокировали особый клуб!");
            BestClass bestClass = new BestClass();
            bestClass.IvanZolo();
            return;
        }

        if (answer == null)
        {
            Console.WriteLine("Ошибка: пустая строка!");
            Environment.Exit(0);
        }
    }
}