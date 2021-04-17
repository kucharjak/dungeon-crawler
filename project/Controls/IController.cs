using Godot;

namespace DungeonCrawler.Controls
{
    public interface IController
    {
        string MoveUp { get; }

        string MoveDown { get; }

        string MoveLeft { get; }

        string MoveRight { get; }

        string Attack { get; }

        string Roll { get; }
        
        string SpecialSkill { get; }
        
        string UsePotion { get; }
        
        bool IsMoveLeftPressed();
        
        bool IsMoveRightPressed();
        
        bool IsMoveUpPressed();
        
        bool IsMoveDownPressed();
        
        bool IsAttackPressed();
        
        bool IsRollPressed();

        bool IsSpecialSkillPressed();

        bool IsUsePotionPressed();

        Vector2 GetInputVector();
        
        bool IsEnabled { get; set; }
    }
}