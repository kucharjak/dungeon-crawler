using DungeonCrawler.Characters.NonPlayable;
using DungeonCrawler.Components.Behaviour;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Components.States.Character.Enemy
{
    public class WanderState : State<EnemyCharacter>
    {
        private Vector2 _wanderPosition;
        private WanderComponent _wanderComponent;

        public WanderState(EnemyCharacter node, Vector2 wanderPosition, WanderComponent wanderComponent) : base(node)
        {
            _wanderPosition = wanderPosition;
            _wanderComponent = wanderComponent;
        }

        public override void Run(float delta)
        {
            if (Node.GetTarget() != null)
            {
                Node.PushState(new FollowState(Node));
                return;
            }

            if (_wanderPosition.DistanceTo(Node.Position) < 5)
            {
                Node.ApplyVelocity(Vector2.Zero);
                Node.PopState();
                return;
            }
            
            var targetDirection = _wanderPosition - Node.Position;
            targetDirection = targetDirection.Normalized();
            
            var newVelocity = Node.Velocity.MoveToward(targetDirection * (Node.MaxSpeed * 0.5f), Node.Acceleration * delta);

            Node.ApplyVelocity(newVelocity);
        }

        public override void End()
        {
            _wanderComponent.Restart(_wanderComponent.WanderTimeout);
        }
    }
}