using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters.Playable.HeroKnight;
using DungeonCrawler.Combat;
using DungeonCrawler.Components.States.Character;
using DungeonCrawler.Components.States.Character.Player;
using DungeonCrawler.Controls;
using DungeonCrawler.Controls.Controllers;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters.Playable
{
    public class PlayerCharacter : Character
    {
        // Exported variables

        // Public properties

        // Protected components
        protected IController Controller = new NoController();

        public override void _Ready()
        {
            base._Ready();
            
            PushState(new MoveState(this));
        }

        public override void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Stats.Hp -= damageAmount;

            if (Stats.Hp > 0)
            {
                AnimationPlayer.Play("Hit");
                InvincibilityComponent.StartInvincibility();
                PushState(new KnockbackState(this, knockbackPower));
                
                return;
            }
                
            
            PushState(new DeathState(this));
        }
        
        public override void StartInvincibility()
        {
            CharacterHurtBox.Disable();
            BlinkAnimationPlayer.Play("Blink");
        }

        public override void EndInvincibility()
        {
            CharacterHurtBox.Enable();
            BlinkAnimationPlayer.ToEnd();
            BlinkAnimationPlayer.Stop();
        }
        
        public override void Die()
        {
            CharacterHurtBox.Disable();
            CharacterHitBox.Disable();
            
            CollisionShape2D.SetDeferred("disabled", true);
            AnimationPlayer.Play("Death");
        }

        public IController GetController() => Controller;

        public void AttachController(IController controller)
        {
            if (controller is null)
            {
                LoggingComponent.Logger.Error($"It's not possible to attach empty controller to object '{GetPath()}'");
                return;
            }

            Controller = controller;
        }

        public bool DetachController()
        {
            if (Controller.GetType() == typeof(NoController))
                return false;

            Controller = new NoController();
            return true;
        }
        
        public override Vector2 ApplyVelocity(Vector2 velocity)
        {
            Velocity = velocity;
            
            var realVelocity = MoveAndSlide(Velocity);

            if (IsRolling())
            {
                AnimationPlayer.Play("Roll");
            } 
            else if (realVelocity != Vector2.Zero) 
            {
                AnimationPlayer.Play("Run");
            }
            else
            {
                AnimationPlayer.Play("Idle");
            }
            
            if (Velocity.x != 0)
            {
                var flip = velocity.x < 0;
            
                CharacterSprite.FlipH = flip;
                CharacterHitBox.Scale = new Vector2(flip ? -1 : 1, CharacterHitBox.Scale.y);
            }

            return realVelocity;
        }

        public bool IsRolling() => AnimationPlayer.CurrentAnimation == "Roll" && AnimationPlayer.IsPlaying();

        public void StartRoll()
        {
            AnimationPlayer.Play("Roll");
            CharacterHurtBox.Disable();
        }

        public void EndRoll()
        {
            CharacterHurtBox.Enable();
        }
    }
}