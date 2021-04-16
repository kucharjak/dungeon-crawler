using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters.Playable.HeroKnight;
using DungeonCrawler.Combat;
using DungeonCrawler.Controls;
using DungeonCrawler.Extensions;
using DungeonCrawler.States.CommonStates;
using DungeonCrawler.States.PlayerStates;
using Godot;

namespace DungeonCrawler.Characters.Playable
{
    public class PlayerCharacter : Character
    {
        // Exported variables

        // Public properties

        // Protected components
        protected IController Controller = new NoController();
        protected InvincibilityComponent InvincibilityComponent;

        public override void _Ready()
        {
            base._Ready();
            
            PushState(new MoveState(this));
            
            HpIndicator = this.GetChildNode<HpIndicator>();
            Stats.Connect(nameof(Stats.HpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeHp));
            Stats.Connect(nameof(Stats.MaxHpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeMaxHP));
            HpIndicator.UpdateHealthIndication();
            
            InvincibilityComponent = this.GetChildNode<InvincibilityComponent>();
            InvincibilityComponent.Connect(nameof(InvincibilityComponent.InvincibilityStarted), this, nameof(StartInvincibility));
            InvincibilityComponent.Connect(nameof(InvincibilityComponent.InvincibilityEnded), this, nameof(EndInvincibility));
        }

        public override void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Stats.Hp -= damageAmount;

            if (Stats.Hp > 0)
            {
                InvincibilityComponent.StartInvincibility();
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
    }
}