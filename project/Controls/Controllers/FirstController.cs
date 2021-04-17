namespace DungeonCrawler.Controls.Controllers
{
    public class FirstController : Controller
    {
        /// <summary>
        /// Default: W | left stick up 
        /// </summary>
        public override string MoveUp { get; } = "MoveUp1";
        
        /// <summary>
        /// Default: S | left stick down 
        /// </summary>
        public override string MoveDown { get; } = "MoveDown1";
        
        /// <summary>
        /// Default: A | left stick left 
        /// </summary>
        public override string MoveLeft { get; } = "MoveLeft1";
         
        /// <summary>
        /// Default: D | left stick right 
        /// </summary>
        public override string MoveRight { get; } = "MoveRight1";
        
        /// <summary>
        /// Default: J | DualShock X
        /// </summary>
        public override string Attack { get; } = "Attack1";
        
        /// <summary>
        /// Default: Space | DualShock Circle 
        /// </summary>
        public override string Roll { get; } = "Roll1";

        /// <summary>
        /// Default: K | DualShock Square
        /// </summary>
        public override string SpecialSkill { get; } = "SpecialSkill1";

        /// <summary>
        /// Default: L | DualShock Triangle
        /// </summary>
        public override string UsePotion { get; } = "UsePotion1";
    }
}