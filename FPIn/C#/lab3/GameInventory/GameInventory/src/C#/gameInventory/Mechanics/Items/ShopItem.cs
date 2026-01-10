using GameInventory.C_.gameInventory.Mechanics.Items;

namespace GameInventory.C_.gameInventory.Shop;

public class ShopItem
{
    public int Number { get; }
    public string Name { get; }
    public ItemType Type { get; }

    public float AttackChange { get; }
    public float ArmorChange { get; }
    public float HealthChange { get; }
    public float SpeedChange { get; }

    public ShopItem(
        int number,
        string name,
        ItemType type,
        float attack,
        float armor,
        float health,
        float speed)
    {
        Number = number;
        Name = name;
        Type = type;
        AttackChange = attack;
        ArmorChange = armor;
        HealthChange = health;
        SpeedChange = speed;
    }

    public string GetDescription()
    {
        return
            $"{Number}. {Name} | " +
            $"Атака: {Format(AttackChange)}, " +
            $"Броня: {Format(ArmorChange)}, " +
            $"Здоровье: {Format(HealthChange)}, " +
            $"Скорость: {Format(SpeedChange)}";
    }

    private string Format(float value)
    {
        if (value > 0) return $"+{value}";
        if (value < 0) return value.ToString();
        return "0";
    }
}