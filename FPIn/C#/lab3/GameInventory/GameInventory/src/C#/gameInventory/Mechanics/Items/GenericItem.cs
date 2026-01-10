using GameInventory.C_.gameInventory.Core;

namespace GameInventory.C_.gameInventory.Mechanics.Items;

public class GenericItem : IItem
{
    public string Name { get; }
    public ItemType Type { get; }
    public bool IsConsumable { get; }

    private readonly float _attackChange;
    private readonly float _armorChange;
    private readonly float _healthChange;
    private readonly float _speedChange;

    public GenericItem (
        string name,
        ItemType type,
        float attackChange,
        float armorChange,
        float healthChange,
        float speedChange,
        bool isConsumable)
    {
        Name = name;
        Type = type;
        IsConsumable = isConsumable;

        _attackChange = attackChange;
        _armorChange = armorChange;
        _healthChange = healthChange;
        _speedChange = speedChange;
    }

    public void Apply(PlayerCharacter player)
    {
        if (_attackChange != 0) { player.AddAttack(_attackChange);}
        if (_armorChange != 0) { player.AddArmor(_armorChange);}

        if (_healthChange != 0)
        {
            if (Type == ItemType.Healing) {player.Heal(_healthChange);}
            else { player.AddMaxHealth(_healthChange);}
        }

        if (_speedChange != 0) { player.AddSpeed(_speedChange);}
    }

    public string GetDescription()
    {
        return $"{Name} | " +
               $"Атака: {_attackChange}, " +
               $"Броня: {_armorChange}, " +
               $"Здоровье: {_healthChange}, " +
               $"Скорость: {_speedChange}";
    }
}