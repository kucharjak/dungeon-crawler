using DungeonCrawler.Characters.Playable;
using Godot;

namespace DungeonCrawler.Components.States.Character.Player
{
    public class MoveState : State<PlayerCharacter>
    {
        public MoveState(PlayerCharacter node) : base(node)
        {
        }

        public override void Run(float delta)
        {
            var controller = Node.GetController();
            if (controller.IsAttackPressed())
            {
                Node.Velocity = Vector2.Zero;
                Node.PushState(new AttackState(Node));
                
                return;
            }
            
            var inputVector = controller.GetInputVector();

            if (inputVector != Vector2.Zero && controller.IsRollPressed())
            {
                Node.PushState(new RollState(Node, inputVector));

                return;
            }
            
            Vector2 newVelocity;
            
            if (inputVector != Vector2.Zero)
            {
                newVelocity = Node.Velocity.MoveToward(inputVector * Node.MaxSpeed, Node.Acceleration * delta);
            }
            else
            {
                newVelocity = Node.Velocity.MoveToward(Vector2.Zero, Node.Friction * delta);
            }
            
            Node.ApplyVelocity(newVelocity);
        }
    }
}