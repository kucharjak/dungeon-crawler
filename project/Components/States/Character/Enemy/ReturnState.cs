using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.Components.States.Character.Enemy
{
    public class ReturnState : State<EnemyCharacter>
    {
        private Vector2 _positionToReturn;

        public ReturnState(EnemyCharacter node, Vector2 positionToReturn) : base(node)
        {
            _positionToReturn = positionToReturn;
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
            
            var newVelocity = Node.Velocity.MoveToward(targetDirection * Node.MaxSpeed, Node.Acceleration * delta);

            Node.ApplyVelocity(newVelocity);
        }
    }
}