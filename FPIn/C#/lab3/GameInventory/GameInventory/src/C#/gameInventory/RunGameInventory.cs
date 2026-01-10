using GameInventory.C_.gameInventory.Core;
using GameInventory.C_.gameInventory.Inventory;
using GameInventory.C_.gameInventory.Mechanics.Items;
using GameInventory.C_.gameInventory.Shop;

namespace GameInventory.C_.gameInventory;

public class RunGameInventory
{
    public static void Main()
    {
        var player = new PlayerCharacter();
        var inventory = new Inventory.Inventory();
        var shop = new Shop.Shop();
        var factory = new ItemFactory();

        while (true)
        {
            Console.WriteLine("\n=== Главное меню ===");
            Console.WriteLine("1 - Магазин");
            Console.WriteLine("2 - Инвентарь");
            Console.WriteLine("3 - Характеристики");
            Console.WriteLine("0 - Выход");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("\nМагазин:");
                    var products = shop.GetProducts();

                    foreach (var p in products)
                        Console.WriteLine(p.GetDescription());

                    Console.Write("\nВыберите номер товара (0 - назад): ");

                    int choice = int.Parse(Console.ReadLine());

                    if (choice <= 0 || choice > 25 || choice == null)
                    {
                        break;
                    }

                    var selected = products.First(p => p.Number == choice);
                    var item = factory.Create(selected);

                    inventory.AddItem(item);
                    inventory.UseLast(player);

                    Console.WriteLine($"\nВы экипировали: {item.Name}");
                    break;

                case "2":
                    Console.WriteLine("\nИнвентарь:");
                    var items = inventory.GetItems();

                    if (!items.Any())
                        Console.WriteLine("Инвентарь пуст");

                    foreach (var it in items)
                        Console.WriteLine(it.GetDescription());
                    break;

                case "3":
                    Console.WriteLine("\nХарактеристики игрока:");
                    Console.WriteLine(player.GetStatsDescription());
                    break;

                case "0":
                    return;
            }
        }
    }
}