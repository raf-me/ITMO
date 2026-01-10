using GameInventory.C_.gameInventory.Mechanics.Items;
using GameInventory.C_.gameInventory.Core;
using GameInventory.C_.gameInventory.Shop;

namespace GameInventory.C_.gameInventory.Mechanics.Items;

public class ItemFactory
{
    public IItem Create(ShopItem item)
    {
        return new GenericItem(
            item.Name,
            item.Type,
            item.AttackChange,
            item.ArmorChange,
            item.HealthChange,
            item.SpeedChange,
            item.Type == ItemType.Healing
        );
    }
}