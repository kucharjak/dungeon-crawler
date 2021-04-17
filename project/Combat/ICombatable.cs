using Godot;

namespace DungeonCrawler.Combat
{
    public interface ICombatable<TCombatant> : IAttackable<TCombatant>, IDestructible
        where TCombatant : Node
    {
    }

    public interface IAttackable<TAttacker>
        where TAttacker : Node
    {
        int GetDamageAmount();

        TAttacker GetAttacker();
    }

    public interface IDestructible 
    {
        void ReceiveDamage(int damageAmount, Vector2 knockbackPower);
    }
}