using GameInventory.C_.gameInventory.Core;
using GameInventory.C_.gameInventory.Mechanics.Items;

namespace GameInventory.C_.gameInventory.Inventory;

public class Inventory
{
    private List<IItem> _items = new();

    public void AddItem(IItem item)
    {
        _items.Add(item);
    }

    public void UseLast(PlayerCharacter player)
    {
        if (_items.Count == 0)
            return;

        var item = _items.Last();
        item.Apply(player);

        if (item.IsConsumable)
            _items.Remove(item);
    }

    public IReadOnlyList<IItem> GetItems()
    {
        return _items;
    }
}