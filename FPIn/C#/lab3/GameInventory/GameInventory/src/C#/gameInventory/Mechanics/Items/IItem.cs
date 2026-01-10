using GameInventory.C_.gameInventory.Core;

namespace GameInventory.C_.gameInventory.Mechanics.Items;

public interface IItem
{
    string Name { get; }
    ItemType Type { get; }
    bool IsConsumable { get; }

    public void Apply(PlayerCharacter stats);
    string GetDescription();
}