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
        protected IController Controller = new NoController();

        internal InvincibilityTimer InvincibilityTimer;
        
        public override void _Ready()
        {
            base._Ready();
            
            PushState(new MoveState(this));
            
            HpIndicator = this.GetChildNode<HpIndicator>();
            Stats.Connect(nameof(Stats.HpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeHp));
            Stats.Connect(nameof(Stats.MaxHpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeMaxHP));
            HpIndicator.UpdateHealthIndication();

            InvincibilityTimer = this.GetChildNode<InvincibilityTimer>();
        }

        public override void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Stats.Hp -= damageAmount;

            if (Stats.Hp <= 0)
            {
                PushState(new DeathState(this));
                return;
            }

            InvincibilityTimer.StartInvincibility();
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