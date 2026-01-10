using GameInventory.C_.gameInventory.Mechanics.Items;

namespace GameInventory.C_.gameInventory.Shop;

public class Shop
{
    private readonly List<ShopItem> products = new()
    {
        new(1, "Кинжал", ItemType.Attack, 5, 0, 0, 1),
        new(2, "Меч", ItemType.Attack, 10, 0, 0, -1),
        new(3, "Арондит", ItemType.Attack, 35, 0, 0, -3),
        new(4, "Нано-меч", ItemType.Attack, 50, -40, 0, +6),

        new(5, "Железная броня", ItemType.Armor, 0, 50, 0, -4),
        new(6, "Нано-блокировка", ItemType.Armor, 0, 25, 0, -1),

        new(7, "Амулет", ItemType.Health, 0, 0, 25, 0),
        new(8, "Рего-клетки", ItemType.Health, 0, 0, 40, -1),

        new(9, "Ласточка", ItemType.Healing, 0, 0, 20, 0),
        new(10, "Зелье Раафа-Белого", ItemType.Healing, 0, 0, 50, 0),

        new(11, "Модификация меча", ItemType.Speed, -10, -10, 0, +4),
        new(12, "Эффект старшей крови", ItemType.Speed, 0, 0, 0, +6)
    };

    public IReadOnlyList<ShopItem> GetProducts() => products;
}