using DungeonCrawler.Controls;
using DungeonCrawler.Controls.Controllers;

namespace DungeonCrawler.Characters.Playable.Amazonian
{
    public class Amazonian : PlayerCharacter
    {
        public override void _Ready()
        {
            base._Ready();
        
            Controller = new SecondController();
        }
    }
}
