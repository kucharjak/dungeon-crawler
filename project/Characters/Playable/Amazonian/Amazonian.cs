using DungeonCrawler.Controls;

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
