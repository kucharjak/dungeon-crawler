using DungeonCrawler.Characters;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class DeathState : State<Character>
    {
        private CollisionShape2D _hurtBoxCollisionShape;
        private CollisionShape2D _softCollisionShape;
        
        public DeathState(Character node) : base(node)
        {
            _hurtBoxCollisionShape = Node.CharacterHurtBox.CollisionShape2D;
            _softCollisionShape = Node.SoftCollision?.CollisionShape2D;
        }

        public override void Init()
        {
            _hurtBoxCollisionShape.SetDeferred("disabled", true);
            _softCollisionShape?.SetDeferred("disabled", true);

            Node.AnimationPlayer.Play("Death");
        }
    }
}