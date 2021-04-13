using System;
using DungeonCrawler.Combat;
using DungeonCrawler.Extensions;
using DungeonCrawler.States;
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

        // Other components
        public Node2D Target;
        public HpIndicator HpIndicator; 

        public override void _Ready()
        {
            base._Ready();
            
            HpIndicator = this.GetChildNode<HpIndicator>();
            Stats.Connect(nameof(Stats.HpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeHp));
            Stats.Connect(nameof(Stats.MaxHpWasChanged), HpIndicator, nameof(HpIndicator.OnChangeMaxHP));
            HpIndicator.UpdateHealthIndication();
            
            PushState(new IdleState(this));

            StartPosition = Position;
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