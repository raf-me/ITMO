using ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;

namespace ConsoleVendingMachine.data.FunctionalManager.MoneyAndPayManager;

public class MoneyManager
{
    Money money = new Money();
    private float balance;

    public void Manager()
    {
        balance = money.DepositMoney();
        Console.WriteLine($"Вы внесли: {balance}₽");
        
        var dataCollection = new DataCollection();

        Console.WriteLine("Введите выбранный товар: ");
        string ProductName = Console.ReadLine();
        
        dataCollection.PayProduct(ProductName, ref balance);
        Console.WriteLine($"Оставшиеся деньги: {balance}₽");
    }
}