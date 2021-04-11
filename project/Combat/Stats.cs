using System;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Combat
{
    public class Stats : Node
    {
        private int _maxHp = 1;
        private int _hp = 1;
        
        [Export]
        public int MaxHp
        {
            get => _maxHp;
            set => this.ChangeValueAndEmitSignal(nameof(MaxHpWasChanged), ref _maxHp, value);
        }
        
        [Export]
        public int Hp
        {
            get => _hp;
            set => this.ChangeValueAndEmitSignal(nameof(HpWasChanged), ref _hp, value);
        }

        [Export] public int AttackDamage = 1;
        [Export] public int SpecialSkillDamage = 1;
    
        [Signal] public delegate void HpWasChanged(int oldValue, int newValue);
        [Signal] public delegate void MaxHpWasChanged(int oldValue, int newValue);
    }
}
