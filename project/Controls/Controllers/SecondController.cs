namespace DungeonCrawler.Controls.Controllers
{
    public class SecondController : Controller
    {
        /// <summary>
        /// Default: Arrow UP | left stick up 
        /// </summary>
        public override string MoveUp { get; } = "MoveUp2";
        
        /// <summary>
        /// Default: Arrow Down | left stick down 
        /// </summary>
        public override string MoveDown { get; } = "MoveDown2";
        
        /// <summary>
        /// Default: Arrow Left | left stick left 
        /// </summary>
        public override string MoveLeft { get; } = "MoveLeft2";
         
        /// <summary>
        /// Default: Arrow Right | left stick right 
        /// </summary>
        public override string MoveRight { get; } = "MoveRight2";
        
        /// <summary>
        /// Default: Numeric 1 | DualShock X
        /// </summary>
        public override string Attack { get; } = "Attack2";
        
        /// <summary>
        /// Default: Numeric 0 | DualShock Circle 
        /// </summary>
        public override string Roll { get; } = "Roll2";

        /// <summary>
        /// Default: Numeric 2 | DualShock Square
        /// </summary>
        public override string SpecialSkill { get; } = "SpecialSkill2";

        /// <summary>
        /// Default: Numeric 3 | DualShock Triangle
        /// </summary>
        public override string UsePotion { get; } = "UsePotion2";
    }
}