using System.Linq;
using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Components.Traits
{
    public class SoftCollision : Area2D
    {
        [Export] public int PushPower = 50; 
        
        internal CollisionShape2D CollisionShape2D;
        
        private Character _character;
    
        public override void _Ready()
        {
            CollisionShape2D = this.GetChildNode<CollisionShape2D>();
            
            _character = this.GetParentNodeRecurse<Character>();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!IsColliding)
                return;

            var velocity = GetPushVector() * PushPower;
            _character.MoveAndSlide(velocity);
        }

        private bool IsColliding => GetOverlappingAreas().Count > 0;

        private Vector2 GetPushVector()
        {
            var pushVector = Vector2.Zero;

            var singleArea = GetOverlappingAreas().Cast<Area2D>().FirstOrDefault();
            if (singleArea != null)
            {
                pushVector = singleArea.GlobalPosition.DirectionTo(_character.GlobalPosition);
                pushVector = pushVector.Normalized();
            }

            return pushVector;
        }
    }
}
