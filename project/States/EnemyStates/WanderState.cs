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
                Node.PopState();
                return;
            }
            
            var targetDirection = _wanderPosition - Node.Position;
            targetDirection = targetDirection.Normalized();

            Node.CharacterSprite.FlipH = targetDirection.x < 0;
            Node.MoveAndSlide(targetDirection * (Node.MaxSpeed * 0.5f));
        }

        public override void End()
        {
            _wanderComponent.Restart();
            Node.AnimationPlayer.Play("Walk");
        }
    }
}