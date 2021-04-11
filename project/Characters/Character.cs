using System.Runtime.InteropServices.ComTypes;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Combat;
using DungeonCrawler.Extensions;
using DungeonCrawler.States;
using DungeonCrawler.States.CommonStates;
using DungeonCrawler.States.EnemyStates;
using Godot;

namespace DungeonCrawler.Characters
{
    public abstract class Character : KinematicBody2D, IStateComponent<IState>, ICombatable<Character>
    {
        // Editable variables
        [Export] public int MaxSpeed = 120;
        [Export] public int Acceleration = 600;
        [Export] public int Friction = 800;
        
        // Character variables
        internal Vector2 Velocity = Vector2.Zero;
        
        // Other components
        internal Sprite CharacterSprite;
        internal AnimationPlayer AnimationPlayer;
        
        internal StateComponent State;
         
        internal Stats Stats;
        internal CharacterHitBox CharacterHitBox;
        internal CharacterHurtBox CharacterHurtBox;
        internal AnimationPlayer BlinkAnimationPlayer;
        
        public override void _Ready()
        {
            CharacterSprite = this.GetChildNode<Sprite>("CharacterSprite");
            AnimationPlayer = this.GetChildNode<AnimationPlayer>();
            
            State = this.GetChildNode<StateComponent>();
            
            Stats = this.GetChildNode<Stats>();
            CharacterHitBox = this.GetChildNode<CharacterHitBox>();
            CharacterHitBox.CollisionShape2D.Disabled = true;
            
            CharacterHurtBox = this.GetChildNode<CharacterHurtBox>();
            BlinkAnimationPlayer = this.GetChildNodeOrNull<AnimationPlayer>(nameof(BlinkAnimationPlayer));
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

        public void ReceiveDamage(int damageAmount, Vector2 knockbackPower)
        {
            Stats.Hp -= damageAmount;

            if (Stats.Hp <= 0)
            {
                PushState(new DeathState(this));
                return;
            }
            
            PushState(new KnockbackState(this, knockbackPower));
        }
    }
}