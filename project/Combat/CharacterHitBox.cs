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
        // Exported variables
        [Export] public int AttackPower = 1;
        [Export] public int KnockbackPower = 200;
        
        // Public properties

        // Protected components
        protected Character Character;
        protected CollisionShape2D CollisionShape2D; 
        
        public override void _Ready()
        {
            Character = this.GetParentNodeRecurse<Character>();
            CollisionShape2D = this.GetChildNode<CollisionShape2D>();

            Connect("area_entered", this, nameof(OnAreaEntered));
        }

        public int GetDamageAmount() => AttackPower;

        public Character GetAttacker() => Character;

        public void OnAreaEntered(object area)
        {
            if (area is IDestructible destructible)
            {
                var damage = GetDamageAmount();
                var knockbackPower = (((Area2D) area).GlobalPosition - Character.GlobalPosition).Normalized();
                knockbackPower *= KnockbackPower;

                destructible.ReceiveDamage(damage, knockbackPower);
            }
        }
        
        public void Disable()
        {
            CollisionShape2D.SetDeferred("disabled", true);
        }

        public void Enable()
        {
            CollisionShape2D.SetDeferred("disabled", false);
        }
    }
}