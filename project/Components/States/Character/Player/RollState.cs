using DungeonCrawler.Characters.Playable;
using Godot;

namespace DungeonCrawler.Components.States.Character.Player
{
    public class RollState : State<PlayerCharacter>
    {
        private readonly Vector2 _rollVector;

        public RollState(PlayerCharacter node, Vector2 rollVector) : base(node)
        {
            _rollVector = rollVector.Normalized();
        }

        public override void Init()
        {
            Node.StartRoll();
        }

        public override void Run(float delta)
        {
            if (!Node.IsRolling())
            {
                Node.PopState();             
                return;
            }
            
            var newVelocity = Node.Velocity.MoveToward(_rollVector * (Node.MaxSpeed * 1.2f), Node.Acceleration * delta);
            Node.ApplyVelocity(newVelocity);
        }

        public override void End()
        {
            Node.EndRoll();
        }
    }
}