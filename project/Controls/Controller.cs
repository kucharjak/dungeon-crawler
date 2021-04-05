using Godot;

namespace DungeonCrawler.Controls
{
    public abstract class Controller : IController
    {
        public abstract string MoveUp { get; }
        public abstract string MoveDown { get; }
        public abstract string MoveLeft { get; }
        public abstract string MoveRight { get; }
        public abstract string Attack { get; }
        public abstract string Roll { get; }
        public abstract string SpecialSkill { get; }
        public abstract string UsePotion { get; }

        public bool IsEnabled { get; set; } = true;

        public bool IsMoveLeftPressed() => IsEnabled && Input.IsActionJustPressed(MoveLeft);
        
        public bool IsMoveRightPressed() => IsEnabled && Input.IsActionJustPressed(MoveRight);

        public bool IsMoveUpPressed() => IsEnabled && Input.IsActionJustPressed(MoveUp);

        public bool IsMoveDownPressed() => IsEnabled && Input.IsActionJustPressed(MoveDown);

        public bool IsAttackPressed() => IsEnabled && Input.IsActionJustPressed(Attack);

        public bool IsRollPressed() => IsEnabled && Input.IsActionJustPressed(Roll);

        public bool IsSpecialSkillPressed() => IsEnabled && Input.IsActionJustPressed(SpecialSkill);
        
        public bool IsUsePotionPressed() => IsEnabled && Input.IsActionJustPressed(UsePotion);

        public Vector2 GetInputVector()
        {
            if (!IsEnabled)
                return Vector2.Zero;
            
            var inputVector = new Vector2(
                Input.GetActionStrength(MoveRight) - Input.GetActionStrength(MoveLeft),
                Input.GetActionStrength(MoveDown) - Input.GetActionStrength(MoveUp));

            return inputVector;
        }
    }
}