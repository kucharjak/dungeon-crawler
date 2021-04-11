using DungeonCrawler.Characters;
using DungeonCrawler.Combat;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class KnockbackState : State<Character>
    {
        private Vector2 _knockbackPower;
        private CollisionShape2D _collisionShape;
        private AnimationPlayer _blinkAnimationPlayer; 

        public KnockbackState(Character node, Vector2 knockbackPower) : base(node)
        {
            _knockbackPower = knockbackPower;
            _collisionShape = Node.CharacterHurtBox.CollisionShape2D;
            _blinkAnimationPlayer = node.BlinkAnimationPlayer;
        }

        public override void Init()
        {
            _collisionShape.SetDeferred("disabled", true);
            _blinkAnimationPlayer?.Play("Blink");
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

        public override void End()
        {
            _collisionShape.SetDeferred("disabled", false);
            _blinkAnimationPlayer?.Stop();
        }
    }
}