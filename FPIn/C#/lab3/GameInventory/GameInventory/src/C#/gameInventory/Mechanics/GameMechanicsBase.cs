namespace GameInventory.C_.gameInventory.Mechanics;

public class GameMechanicsBase
{
    private float attack = 20;
    private float armor = 0;
    private float maxHealth = 100;
    private float health = 100;
    private float speed = 10;

    internal float GetAttack() => attack;
    internal float GetArmor() => armor;
    internal float GetHealth() => health;
    internal float GetMaxHealth() => maxHealth;
    internal float GetSpeed() => speed;

    internal void AddAttack(float value) => attack += value;
    internal void AddArmor(float value) => armor += value;

    internal void AddMaxHealth(float value)
    {
        maxHealth += value;
        health += value;
    }

    internal void Heal(float value)
    {
        health += value;
        if (health > maxHealth)
            health = maxHealth;
    }

    internal void AddSpeed(float value) => speed += value;
}