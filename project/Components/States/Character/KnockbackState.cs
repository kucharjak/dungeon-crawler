using Godot;

namespace DungeonCrawler.Components.States.Character
{
    public class KnockbackState : State<Characters.Character>
    {
        private Vector2 _knockbackPower;

        public KnockbackState(Characters.Character node, Vector2 knockbackPower) : base(node)
        {
            _knockbackPower = knockbackPower;
        }

        public override void Run(float delta)
        {
            if (_knockbackPower == Vector2.Zero)
            {
                Node.PopState();
                return;
            }
            
            _knockbackPower = _knockbackPower.MoveToward(Vector2.Zero, Node.Friction * delta);
            Node.MoveAndSlide(_knockbackPower);
        }
    }
}