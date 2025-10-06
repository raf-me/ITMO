using ConsoleVendingMachine.data.Main;
using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;

namespace ConsoleVendingMachine.data.FunctionalManager.MoneyAndPayManager;

public class Money
{
    private int moneyClient;

    public int DepositMoney()
    {
        Console.WriteLine("Внесите купюру: "); ;
        
        while (!int.TryParse(Console.ReadLine(), out moneyClient) || moneyClient <= 0)
            Console.Write("Некорректно. Введите положительное число: ");
        return moneyClient;
    }
}