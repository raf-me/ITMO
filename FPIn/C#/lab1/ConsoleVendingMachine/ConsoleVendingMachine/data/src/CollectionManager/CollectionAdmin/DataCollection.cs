using System.Text.Json;
using ConsoleVendingMachine.data.CollectionManager.CollectionData;
using ConsoleVendingMachine.data.FunctionalManager.AdminManager;

namespace ConsoleVendingMachine.data.CollectionManager.CollectionAdmin;

public class DataCollection : ReadJSONforAssortments
{ 
    public List<AssortmentCollection> AssortmentCollections {get; private set;}

    public DataCollection(string fileName = "data.json") : base(fileName)
    {
        AssortmentCollections = LoadAssortment();
    }

    public void AddProduct(string name, float price, int quantity)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя товара не может быть пустым!");
        
            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше 0!");
        
            if (quantity < 0)
                throw new ArgumentException("Количество не может быть отрицательным!");

            AssortmentCollections.Add(new AssortmentCollection
            {
                Name = name,
                Price = price,
                Quantity = quantity
            });
            
            Console.WriteLine($"Товар \"{name}\" успешно добавлен!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка ввода: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при добавлении товара: {ex.Message}");
        }
    }

    public void RemoveProduct(string name)
    {
        var product = AssortmentCollections.FirstOrDefault(
            p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product != null)
        {
            AssortmentCollections.Remove(product);
            Console.WriteLine($"Товар \"{name}\" успешно удалён!");
        }
    }

    public void PayProduct(string productName, ref float price)
    {
        var product = AssortmentCollections.FirstOrDefault(
            p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (product == null)
        {
            Console.WriteLine("Товар не найден! Попробуйте снова:");
            Console.Write("Введите название товара: ");
            string newName = Console.ReadLine();
            PayProduct(newName, ref price);
            return;
        }

        if (product.Quantity <= 0)
        {
            Console.WriteLine($"Товар \"{productName}\" закончился. Попробуйте выбрать другой.");
            Console.Write("Введите другой товар: ");
            string newName = Console.ReadLine();
            PayProduct(newName, ref price);
            return;
        }

        if (product.Price > price)
        {
            Console.WriteLine("Недостаточно средств для покупки!");
            Console.Write("Введите название другого товара: ");
            string newName = Console.ReadLine();
            PayProduct(newName, ref price);
            return;
        }
        
        product.Quantity -= 1;
        price -= product.Price;
        Console.WriteLine($"Вы купили товар \"{product.Name}\" за {product.Price}₽. Остаток средств: {price}₽");

        if (product.Quantity == 0)
        {
            Console.WriteLine($"Товар \"{product.Name}\" закончился и временно недоступен для дальнейших покупок.");
        }
    }
}