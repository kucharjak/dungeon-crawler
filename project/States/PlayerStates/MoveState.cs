using DungeonCrawler.Characters;
using DungeonCrawler.Characters.Playable;
using DungeonCrawler.Controls;
using Godot;

namespace DungeonCrawler.States.PlayerStates
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