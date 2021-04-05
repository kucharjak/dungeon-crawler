using Godot;

namespace DungeonCrawler.Combat
{
    public class Stats : Node
    {
        [Export] public int Hp = 1;
        [Export] public int MaxHp = 1;
        
        [Export] public int AttackDamage = 1;
        [Export] public int SpecialSkillDamage = 1;
    
        [Signal] public delegate void HpWasChanged(int oldValue, int newValue);
        [Signal] public delegate void MaxHpWasChanged(int oldValue, int newValue);
    }
}
