using ConsoleVendingMachine.data.Main;
using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;

namespace ConsoleVendingMachine.data.FunctionalManager.MoneyAndPayManager;

public class Money
{
    private float moneyClient;

    public float DepositMoney()
    {
        Console.Write("Внесите купюру: ");
        moneyClient = float.Parse(Console.ReadLine());

        if (moneyClient <= 0)
        {
            throw new ArgumentException("Неверный формат!");
        }
        
        return moneyClient;
    }
}