using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.Components.States.Character.Enemy
{
    public class KnockbackState : State<EnemyCharacter>
    {
        private Vector2 _knockbackPower;

        public KnockbackState(EnemyCharacter node, Vector2 knockbackPower) : base(node)
        {
            _knockbackPower = knockbackPower;
        }

        public override void Init()
        {
            Node.StartInvincibility();
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
            Node.EndInvincibility();   
        }
    }
}