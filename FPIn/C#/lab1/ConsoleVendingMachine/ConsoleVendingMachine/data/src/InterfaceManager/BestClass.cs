namespace ConsoleVendingMachine.data.InterfaceManager;

public class BestClass
{
    public void IvanZolo()
    {
        Console.WriteLine("Добро пожаловать в пельменную!");
        Console.WriteLine("Выберите действие: \n" +
                          "1. Зайти в пельменную.\n" +
                          "2. Подписаться в ТГ\n" +
                          "3. Заглянуть в будущее...\n" +
                          "exit - поуинть священное место...");

        string answer = Console.ReadLine();

        if (answer == "1")
        {
            Console.WriteLine("https://m.twitch.tv/zubarefff/home");
            Console.WriteLine("Вперёд в мир приключений!");
            IvanZolo();
            return;
        }

        if (answer == "2")
        {
            Console.WriteLine("https://t.me/s/zubarefff");
            Console.WriteLine("Новости пельменной");
            IvanZolo();
            return;
        }

        if (answer == "3")
        {
            Console.WriteLine("https://www.youtube.com/watch?v=Gkf5AZsHc74");
            Console.WriteLine("Удачи)");
            IvanZolo();
            return;
        }

        if (answer == "exit")
        {
            Environment.Exit(0);
        }
    }
}