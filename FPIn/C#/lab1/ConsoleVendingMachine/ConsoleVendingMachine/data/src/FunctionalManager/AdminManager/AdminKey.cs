using System.Text.Json;
using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;
using ConsoleVendingMachine.data.Main;

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
            Console.WriteLine("Выберите действие (Add, Remove или Cash): ");
            string answer = Console.ReadLine();

            if (answer != "Add" && answer != "Remove" && answer != "Cash") throw new ArgumentException();

            if (answer == "Add")
            {
                assortment.AddManager();
                Console.WriteLine("Продолжить работу? (да, нет, перейти в начало)");

                if (answer == "да")
                {
                    AdminWork();
                    return;
                }

                if (answer == "нет")
                {
                    Environment.Exit(0);
                    return;
                }

                if (answer == "перейти в начало")
                {
                    Menu menu = new Menu();
                    menu.MenuChoice();
                    return;
                }
            }

            if (answer == "Remove")
            {
                assortment.RemoveManager();
                Console.WriteLine("Продолжить работу? (да, нет, перейти в начало)");

                if (answer == "да")
                {
                    AdminWork();
                    return;
                }

                if (answer == "нет")
                {
                    Environment.Exit(0);
                    return;
                }

                if (answer == "перейти в начало")
                {
                    Menu menu = new Menu();
                    menu.MenuChoice();
                    return;
                }
            }

            if (answer == "Cash")
            {
                string moneyPath = Path.GetFullPath("../../../data/recources/money.json");
                
                var json = File.ReadAllText(moneyPath);
                var moneyList = JsonSerializer.Deserialize<List<Dictionary<string, float>>>(json);
                
                if (moneyList != null && moneyList.Count > 0 && moneyList[0].ContainsKey("money"))
                {
                    Console.WriteLine($"Собрано {moneyList[0]["money"]} выручки");
                    moneyList[0]["money"] = 0;
                }

                string updatedJson = JsonSerializer.Serialize(moneyList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(moneyPath, updatedJson);
                return;
            }
        }

        else
        {
            Console.WriteLine("Неверное имя или пароль!");
            AdminWork();
        }
    }
}