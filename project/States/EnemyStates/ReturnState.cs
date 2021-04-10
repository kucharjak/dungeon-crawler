using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.States.EnemyStates
{
    public class ReturnState : State<EnemyCharacter>
    {
        private Vector2 _positionToReturn;

        public ReturnState(EnemyCharacter node, Vector2 positionToReturn) : base(node)
        {
            _positionToReturn = positionToReturn;
        }

        public override void Init()
        {
            Node.AnimationPlayer.Play("Run");
        }

        public override void Run(float delta)
        {
            if (_positionToReturn.DistanceTo(Node.Position) < 5)
            {
                Node.PopState();
                return;
            }
            
            var targetDirection = _positionToReturn - Node.Position;
            targetDirection = targetDirection.Normalized();

            Node.CharacterSprite.FlipH = targetDirection.x < 0;
            Node.MoveAndSlide(targetDirection * Node.MaxSpeed);
        }

        public override void Resume()
        {
            Node.AnimationPlayer.Play("Run");
        }
    }
}