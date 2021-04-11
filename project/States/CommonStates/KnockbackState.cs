using DungeonCrawler.Characters;
using DungeonCrawler.Combat;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class KnockbackState : State<Character>
    {
        private Vector2 _knockbackPower;
        private CollisionShape2D _collisionShape;
        private AnimationPlayer _animationPlayer; 
        private AnimationPlayer _blinkAnimationPlayer; 

        public KnockbackState(Character node, Vector2 knockbackPower) : base(node)
        {
            _knockbackPower = knockbackPower;
            _collisionShape = Node.CharacterHurtBox.CollisionShape2D;
            _animationPlayer = Node.AnimationPlayer;
            _blinkAnimationPlayer = node.BlinkAnimationPlayer;
        }

        public override void Init()
        {
            _collisionShape.SetDeferred("disabled", true);
            _blinkAnimationPlayer?.Play("Blink");
            _animationPlayer.Play("Hit");
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
            _blinkAnimationPlayer?.ForwardEnd();
            _blinkAnimationPlayer?.Stop();
        }
    }
}