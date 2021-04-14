using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Combat
{
    public class InvincibilityTimer : Timer
    {
        private Character _character;
        
        public override void _Ready()
        {
            _character = this.GetParentNodeRecurse<Character>();

            Connect("timeout", this, nameof(OnInvincibilityTimeout));
        }

        public void OnInvincibilityTimeout()
        {
            EndInvincibility();
        }
        
        public void StartInvincibility()
        {
            _character.CharacterHurtBox.CollisionShape2D.SetDeferred("disabled", true);
            _character.BlinkAnimationPlayer?.Play("Blink");
            Start();
        }
        
        public void EndInvincibility()
        {
            _character.CharacterHurtBox.CollisionShape2D.SetDeferred("disabled", false);
            _character.BlinkAnimationPlayer?.ToEnd();
            _character.BlinkAnimationPlayer?.Stop();
        }
    }
}
