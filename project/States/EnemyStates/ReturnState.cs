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
            
            Node.Velocity = Node.Velocity.MoveToward(targetDirection * Node.MaxSpeed, Node.Acceleration * delta);

            if (Node.Velocity.x != 0)
            {
                var flip = Node.Velocity.x < 0;

                Node.CharacterSprite.FlipH = flip;
                Node.CharacterHitBox.Scale = new Vector2(flip ? -1 : 1, Node.CharacterHitBox.Scale.y);
            }
            
            Node.MoveAndSlide(Node.Velocity);
        }

        public override void Resume()
        {
            Node.AnimationPlayer.Play("Run");
        }
    }
}