using System;
using DungeonCrawler.Combat;
using DungeonCrawler.Components;
using DungeonCrawler.Components.States.Character;
using DungeonCrawler.Components.States.Character.Enemy;
using DungeonCrawler.Components.Traits;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable
{
    public class EnemyCharacter : Character
    {
        // Editable variables
        [Export] public bool IsAggressive = true;

        // Public properties
        public Vector2 StartPosition = Vector2.Zero;

        // Protected components
        protected SoftCollision SoftCollision;
        protected DetectionZone DetectionZone;
        protected Node2D Target;

        public override void _Ready()
        {
            base._Ready();
            
            SoftCollision = this.GetChildNodeOrNull<SoftCollision>();

            DetectionZone = this.GetChildNode<DetectionZone>();
            DetectionZone.Connect(nameof(DetectionZone.TargetSpotted), this, nameof(OnTargetSpotted));
            // DetectionZone.Connect(nameof(DetectionZone.TargetLost), this, nameof(OnTargetLost));
            
            PushState(new IdleState(this));

            StartPosition = Position;
            
            HpIndicator.UpdateHealthIndication();
        }

        public override void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Stats.Hp -= damageAmount;

            if (Stats.Hp <= 0)
            {
                PushState(new DeathState(this));
                return;
            }
            
            PushState(new KnockbackState(this, knockbackPower));
        }

        public override void Die()
        {
            CharacterHurtBox.Disable();
            CharacterHitBox.Disable();
            
            CollisionShape2D.SetDeferred("disabled", true);
            SoftCollision.CollisionShape2D.SetDeferred("disabled", true);

            AnimationPlayer.Play("Death");
        }

        public override void StartInvincibility()
        {
            CharacterHurtBox.Disable();
            BlinkAnimationPlayer.Play("Blink");
            AnimationPlayer.Play("Hit");
        }

        public override void EndInvincibility()
        {
            CharacterHurtBox.Enable();
            BlinkAnimationPlayer.ToEnd();
            BlinkAnimationPlayer.Stop();
        }

        public Node2D GetTarget() => Target;

        public void OnTargetSpotted(Node2D target)
        {
            if (IsAggressive == false)
                return;
            
            Target = target;
        }
    
        public void OnTargetLost(Node2D target)
        {
            if (target != Target)
                return;
            
            Target = null;
        }
    }
}