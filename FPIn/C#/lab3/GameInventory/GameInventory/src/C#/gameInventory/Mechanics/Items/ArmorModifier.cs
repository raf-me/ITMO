using GameInventory.C_.gameInventory.Mechanics;

namespace GameInventory.C_.gameInventory.Mechanics.Items;

public class ArmorModifier : StatsModifier
{
    private readonly float armorBonus;

    public ArmorModifier(float armorBonus)
    {
        this.armorBonus = armorBonus;
    }

    public float ModifyDamage(float baseDamage) => baseDamage;
    public float ModifyArmor(float baseArmor) => baseArmor + armorBonus;
}