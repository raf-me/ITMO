using GameInventory.C_.gameInventory.Mechanics.Items;

namespace GameInventory.C_.gameInventory.Builder;

public class UpgradeBuilder
{
    private float bonus;

    public UpgradeBuilder AddLevel()
    {
        bonus += 5;
        return this;
    }

    public WeaponModifier BuildWeapon()
        => new WeaponModifier(bonus);
}