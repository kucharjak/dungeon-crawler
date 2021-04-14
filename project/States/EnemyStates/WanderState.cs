using DungeonCrawler.Characters.NonPlayable;
using DungeonCrawler.Components;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.States.EnemyStates
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

        public override void Init()
        {
            Node.AnimationPlayer.Play("Walk");
        }

        public override void Run(float delta)
        {
            if (_wanderPosition.DistanceTo(Node.Position) < 5)
            {
                Node.Velocity = Vector2.Zero;
                Node.PopState();
                return;
            }
            
            var targetDirection = _wanderPosition - Node.Position;
            targetDirection = targetDirection.Normalized();
            
            Node.Velocity = Node.Velocity.MoveToward(targetDirection * (Node.MaxSpeed * 0.5f), Node.Acceleration * delta);

            if (Node.Velocity.x != 0)
            {
                var flip = Node.Velocity.x < 0;

                Node.CharacterSprite.FlipH = flip;
                Node.CharacterHitBox.Scale = new Vector2(flip ? -1 : 1, Node.CharacterHitBox.Scale.y);
            }
            
            Node.MoveAndSlide(Node.Velocity);
        }

        public override void End()
        {
            _wanderComponent.Restart();
            Node.AnimationPlayer.Play("Walk");
        }
    }
}