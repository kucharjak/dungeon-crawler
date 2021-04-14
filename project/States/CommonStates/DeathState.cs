using DungeonCrawler.Characters;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class DeathState : State<Character>
    {
        public DeathState(Character node) : base(node)
        {
        }

        public override void Init()
        {
            Node.CollisionShape2D.SetDeferred("disabled", true);
            Node.CharacterHurtBox.CollisionShape2D.SetDeferred("disabled", true);
            Node.SoftCollision?.CollisionShape2D.SetDeferred("disabled", true);

            Node.AnimationPlayer.Play("Death");
        }
    }
}