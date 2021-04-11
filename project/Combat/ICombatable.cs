using Godot;

namespace DungeonCrawler.Combat
{
    public enum AttackType
    {
        Basic,
        Special
    }
    
    public interface ICombatable<TCombatant> : IAttackable<TCombatant>, IDestructible
        where TCombatant : Node
    {
    }

    public interface IAttackable<TAttacker>
        where TAttacker : Node
    {
        int GetDamageAmount(AttackType attackType);

        TAttacker GetAttacker();
    }

    public interface IDestructible 
    {
        void ReceiveDamage(int damageAmount, Vector2 knockbackPower);
    }
}