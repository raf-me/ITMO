// See https://aka.ms/new-console-template for more information

using DeliveryService.C_.deliveryService.data;

public class Program
{
    public static void Main(string[] args)
    {
        OrderManager manager = new OrderManager();
        manager.Start();
    }
}