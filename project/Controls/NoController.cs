using Godot;

namespace DungeonCrawler.Controls
{
    public class NoController : IController
    {
        public string MoveUp { get; } = "";
        
        public string MoveDown { get; } = "";
        
        public string MoveLeft { get; } = "";
        
        public string MoveRight { get; } = "";
        
        public string Attack { get; } = "";
        
        public string Roll { get; } = "";
        
        public string SpecialSkill { get; } = "";

        public string UsePotion { get; } = "";

        public bool IsMoveLeftPressed() => false;

        public bool IsMoveRightPressed() => false;

        public bool IsMoveUpPressed() => false;

        public bool IsMoveDownPressed() => false;

        public bool IsAttackPressed() => false;

        public bool IsRollPressed() => false;

        public bool IsSpecialSkillPressed() => false;

        public bool IsUsePotionPressed() => false;

        public Vector2 GetInputVector() => Vector2.Zero;

        public bool IsEnabled { get; set; } 
    }
}