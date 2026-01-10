namespace GameInventory.C_.gameInventory.Mechanics;

public interface StatsModifier
{
    float ModifyDamage(float baseDamage);
    float ModifyArmor(float baseArmor);
}