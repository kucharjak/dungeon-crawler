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
            var inputVector = controller.GetInputVector();
            
            if (inputVector != Vector2.Zero)
            {
                Node.AnimationPlayer.Play("Run");
                Node.Velocity = Node.Velocity.MoveToward(inputVector * Node.MaxSpeed, Node.Acceleration * delta);
            }
            else
            {
                Node.AnimationPlayer.Play("Idle");
                Node.Velocity = Node.Velocity.MoveToward(Vector2.Zero, Node.Friction * delta);
            }
            
            if (Node.Velocity.x != 0)
                Node.CharacterSprite.FlipH = Node.Velocity.x < 0;

            Attack(controller);
            
            Node.MoveAndSlide(Node.Velocity);
        }
        
        private void Attack(IController controller)
        {
            if (!controller.IsAttackPressed())
                return;
            
            Node.Velocity = Vector2.Zero;
            Node.PushState(new AttackState(Node));
        }
    }
}