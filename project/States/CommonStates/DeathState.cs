using DungeonCrawler.Characters;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class DeathState : State<Character>
    {
        private readonly CollisionShape2D _collisionShape;
        
        public DeathState(Character node) : base(node)
        {
            _collisionShape = Node.CharacterHurtBox.CollisionShape2D;
        }

        public override void Init()
        {
            _collisionShape.SetDeferred("disabled", true);
            Node.AnimationPlayer.Play("Death");
        }
    }
}