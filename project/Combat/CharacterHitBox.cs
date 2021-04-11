using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using DungeonCrawler.Logging;
using Godot;

namespace DungeonCrawler.Combat
{
    public class CharacterHitBox : Area2D, IAttackable<Character>
    {
        [Export] public AttackType AttackType = AttackType.Basic;
        [Export] public int KnockbackPower = 200;
        
        protected Character Character;

        // Children nodes
        internal CollisionShape2D CollisionShape2D; 
        
        public override void _Ready()
        {
            Character = this.GetParentNodeRecurse<Character>();
            
            CollisionShape2D = this.GetChildNode<CollisionShape2D>();
        }

        public int GetDamageAmount(AttackType attackType)
        {
            return Character.GetDamageAmount(attackType);
        }

        public Character GetAttacker()
        {
            return Character.GetAttacker();
        }

        public void OnAreaEntered(object area)
        {
            if (area is IDestructible destructible)
            {
                var damage = GetDamageAmount(AttackType.Basic);
                var knockbackPower = (((Area2D) area).GlobalPosition - Character.GlobalPosition).Normalized();
                knockbackPower *= KnockbackPower;

                destructible.ReceiveDamage(damage, knockbackPower);
            }
        }
    }
}