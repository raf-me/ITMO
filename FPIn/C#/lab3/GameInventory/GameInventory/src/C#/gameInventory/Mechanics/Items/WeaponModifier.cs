using GameInventory.C_.gameInventory.Mechanics;

namespace GameInventory.C_.gameInventory.Mechanics.Items;

public class WeaponModifier : StatsModifier
{
    private readonly float damageBonus;

    public WeaponModifier(float damageBonus)
    {
        this.damageBonus = damageBonus;
    }

    public float ModifyDamage(float baseDamage) => baseDamage + damageBonus;
    public float ModifyArmor(float baseArmor) => baseArmor;
}