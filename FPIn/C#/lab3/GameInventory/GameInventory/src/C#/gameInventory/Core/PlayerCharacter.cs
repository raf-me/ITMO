using GameInventory.C_.gameInventory.Mechanics;

namespace GameInventory.C_.gameInventory.Core;

public class PlayerCharacter
{
    private readonly GameMechanicsBase stats = new();

    public float Attack => stats.GetAttack();
    public float Armor => stats.GetArmor();
    public float Health => stats.GetHealth();
    public float MaxHealth => stats.GetMaxHealth();
    public float Speed => stats.GetSpeed();

    public void AddAttack(float value) => stats.AddAttack(value);
    public void AddArmor(float value) => stats.AddArmor(value);
    public void AddMaxHealth(float value) => stats.AddMaxHealth(value);
    public void Heal(float value) => stats.Heal(value);
    public void AddSpeed(float value) => stats.AddSpeed(value);

    public string GetStatsDescription()
    {
        return
            $"Атака: {Attack}\n" +
            $"Броня: {Armor}\n" +
            $"Здоровье: {Health} / {MaxHealth}\n" +
            $"Скорость: {Speed}";
    }
}