using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Combat
{
    public class CharacterHurtBox : Area2D, IDestructible
    {
        // Public properties
        [Signal] public delegate void DamageReceived(int damageAmount, Vector2 knockbackPower);

        // Protected components
        protected CollisionShape2D CollisionShape2D;
        
        public override void _Ready()
        {
            CollisionShape2D = this.GetChildNode<CollisionShape2D>();
        }
        
        public void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            EmitSignal(nameof(DamageReceived), damageAmount, knockbackPower);
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