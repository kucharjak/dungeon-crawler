using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Combat;
using DungeonCrawler.Components;
using DungeonCrawler.Components.States;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters
{
    public abstract class Character : KinematicBody2D, IStateComponent<IState>, ICombatable<Character>
    {
        // Exported variables
        [Export] public int MaxSpeed = 120;
        [Export] public int Acceleration = 600;
        [Export] public int Friction = 800;
        [Export] public float InvincibilityTimeout = 0.5f;
        
        // Public character variables
        public Vector2 Velocity = Vector2.Zero;
        
        // Protected components
        protected Sprite CharacterSprite;
        protected AnimationPlayer AnimationPlayer;
        protected CollisionShape2D CollisionShape2D;
        
        protected StateComponent State;
         
        protected Stats Stats;
        protected CharacterHitBox CharacterHitBox;
        protected CharacterHurtBox CharacterHurtBox;
        protected AnimationPlayer BlinkAnimationPlayer;
        
        protected HpIndicator HpIndicator;
        
        public override void _Ready()
        {
            CharacterSprite = this.GetChildNode<Sprite>(nameof(CharacterSprite));
            AnimationPlayer = this.GetChildNode<AnimationPlayer>();
            CollisionShape2D = this.GetChildNode<CollisionShape2D>(nameof(CollisionShape2D));
            
            State = this.GetChildNode<StateComponent>();
            
            Stats = this.GetChildNode<Stats>();
            CharacterHitBox = this.GetChildNode<CharacterHitBox>();
            CharacterHitBox.CollisionShape2D.Disabled = true;
            
            CharacterHurtBox = this.GetChildNode<CharacterHurtBox>();
            BlinkAnimationPlayer = this.GetChildNodeOrNull<AnimationPlayer>(nameof(BlinkAnimationPlayer));

            CharacterHurtBox.Connect(nameof(CharacterHurtBox.DamageReceived), this, nameof(ReceiveDamage));
            
            HpIndicator = this.GetChildNode<HpIndicator>();
            Stats.Connect(nameof(Stats.HpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeHp));
            Stats.Connect(nameof(Stats.MaxHpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeMaxHP));
            HpIndicator.UpdateHealthIndication();
        }

        public virtual Vector2 ApplyVelocity(Vector2 velocity)
        {
            Velocity = velocity;
            
            var realVelocity = MoveAndSlide(Velocity);
            
            if (realVelocity != Vector2.Zero)
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
        
        public int StatesCount => State.StatesCount;
        
        public void PushState(IState newState)
        {
            State.PushState(newState);
        }

        public IState PopState()
        {
            return State.PopState();
        }

        public IState PeekState()
        {
            return State.PeekState();
        }

        public int GetHpValue() => Stats.Hp;

        public int GetMaxHpValue() => Stats.MaxHp;

        public int GetDamageAmount(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Basic: 
                    return Stats.AttackDamage;
                case AttackType.Special: 
                    return Stats.SpecialSkillDamage;
            }
            
            LoggingComponent.Logger.Error($"{GetType().Name} - Attack type of {attackType} is not implemented");
            return 0;
        }

        public Character GetAttacker()
        {
            return this;
        }

        public abstract void ReceiveDamage(int damageAmount, Vector2 knockbackPower);

        public abstract void Die();

        public abstract void StartInvincibility();

        public abstract void EndInvincibility();

        public bool IsAttacking => AnimationPlayer.CurrentAnimation == "Attack" && AnimationPlayer.IsPlaying();
        
        public void Attack()
        {
            AnimationPlayer.Play("Attack");
        }
    }
}