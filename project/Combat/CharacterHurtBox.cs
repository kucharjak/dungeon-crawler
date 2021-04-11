using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Combat
{
    public class CharacterHurtBox : Area2D, IDestructible
    {
        protected Character Character;

        // Children nodes
        internal CollisionShape2D CollisionShape2D; 
        
        public override void _Ready()
        {
            Character = this.GetParentNodeRecurse<Character>();
            
            CollisionShape2D = this.GetChildNode<CollisionShape2D>();
        }

        public void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Character.ReceiveDamage(damageAmount, knockbackPower);
        }
    }
}