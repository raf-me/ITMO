namespace DeliveryService.C_.deliveryService.data;

public class OrderManager
{
    private Dictionary<int, Order> Orders;
    private Dictionary<int, (string, decimal)> Menu;
    private int orderID;

    public OrderManager()
    {
        Orders = new Dictionary<int, Order>();
        orderID = 1000;

        Menu = new Dictionary<int, (string, decimal)>();
        Menu.Add(1, ("Бургер", 500));
        Menu.Add(2, ("Пицца", 350));
        Menu.Add(3, ("Салат", 200));
    }

    public void Start()
    {
        Console.WriteLine("\n=== Система доставки еды ===");
        Console.WriteLine("1 - Создать заказ");
        Console.WriteLine("2 - Посмотреть чек заказа");
        Console.WriteLine("3 - Изменить статус заказа");
        Console.WriteLine("0 - Выход");

        string cmd = Console.ReadLine();

        if (cmd == "1") { CreateOrder();}
        if (cmd == "2") { ShowCheck();}
        if (cmd == "3") { ChangeStatus();}
        if (cmd == "0") { return;}

        Start();
    }

    private void CreateOrder()
    {
        OrderFactory factory = new StandardOrderFactory();
        Order order = factory.CreateOrder();
        order.OrderID = ++orderID;

        OrderBuilder builder = new OrderBuilder(order);
        
        while (true)
        {
            Console.WriteLine("\nМеню:");
            foreach (var item in Menu)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Item1} ({item.Value.Item2} руб)");
            }

            Console.WriteLine("Введите номер блюда (0 - закончить выбор):");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0) { break;}

            if (Menu.ContainsKey(choice))
            {
                var dish = Menu[choice];
                builder.AddDish(dish.Item1, dish.Item2);
                Console.WriteLine("Блюдо добавлено");
            }
        }
        
        Console.WriteLine("Тип доставки (1 - обычная, 2 - быстрая):");
        string delivery = Console.ReadLine();
        order.IsFast = delivery == "2";
        
        Console.WriteLine("Применить скидку? (да / нет):");
        order.UseDiscount = Console.ReadLine().ToLower() == "да";

        CalculatePrice(order);

        Orders.Add(order.OrderID, builder.Build());
        Console.WriteLine($"Заказ создан. ID: {order.OrderID}");
    }
    

    private void CalculatePrice(Order order)
    {
        AppConfig config = AppConfig.GetInstance();

        decimal total = order.BasePrice;
        total += config.DeliveryPrice;

        if (order.IsFast) { total += config.FastDeliveryExtra;}

        total += total * config.Tax;

        if (order.UseDiscount) { total -= total * config.Discount;}

        order.TotalPrice = total;
    }
    
    private void ShowCheck()
    {
        Console.WriteLine("Введите ID заказа:");
        int id = int.Parse(Console.ReadLine());

        if (!Orders.ContainsKey(id))
        {
            Console.WriteLine("Заказ не найден");
            return;
        }

        Order o = Orders[id];
        AppConfig config = AppConfig.GetInstance();

        Console.WriteLine("\n===== ЧЕК =====");
        foreach (var d in o.Dishes) { Console.WriteLine(d);}

        Console.WriteLine($"Сумма блюд: {o.BasePrice}");
        Console.WriteLine($"Доставка: {config.DeliveryPrice}");

        if (o.IsFast) { Console.WriteLine($"Быстрая доставка: {config.FastDeliveryExtra}");}

        Console.WriteLine($"НДС: {config.Tax * 100}%");

        if (o.UseDiscount) { Console.WriteLine($"Скидка: {config.Discount * 100}%");}

        Console.WriteLine($"ИТОГО: {o.TotalPrice}");
        Console.WriteLine($"Статус: {o.State.GetStatus()}");
    }

    private void ChangeStatus()
    {
        Console.WriteLine("Введите ID заказа:");
        int id = int.Parse(Console.ReadLine());

        if (!Orders.ContainsKey(id))
        {
            Console.WriteLine("Заказ не найден");
            return;
        }

        Console.WriteLine("1 - Готовится");
        Console.WriteLine("2 - Доставляется");
        Console.WriteLine("3 - Выполнен");

        string s = Console.ReadLine();

        if (s == "1") { Orders[id].State = new PreparingState();}
        if (s == "2") { Orders[id].State = new DeliveringState();}
        if (s == "3") { Orders[id].State = new CompletedState();}
    }
}