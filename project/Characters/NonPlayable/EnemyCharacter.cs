using System;
using DungeonCrawler.Combat;
using DungeonCrawler.Extensions;
using DungeonCrawler.States;
using DungeonCrawler.States.CommonStates;
using DungeonCrawler.States.EnemyStates;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable
{
    public class EnemyCharacter : Character
    {
        private bool _isAggressive = true;
        
        // Editable variables
        [Export]
        public bool IsAggressive
        {
            get { return _isAggressive; }
            set
            {
                if (value == false) Target = null;
                _isAggressive = value;
            }
        }

        // Enemy properties
        internal Vector2 StartPosition = Vector2.Zero;

        internal DetectionZone DetectionZone;

        // Other components
        public Node2D Target; 

        public override void _Ready()
        {
            base._Ready();
            
            HpIndicator = this.GetChildNode<HpIndicator>();
            Stats.Connect(nameof(Stats.HpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeHp));
            Stats.Connect(nameof(Stats.MaxHpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeMaxHP));

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