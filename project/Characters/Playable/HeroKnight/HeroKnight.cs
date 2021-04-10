using DungeonCrawler.Controls;

namespace DungeonCrawler.Characters.Playable.HeroKnight
{
    public class HeroKnight : Character
    {
        public override void _Ready()
        {
            base._Ready();
            
            Controller = new FirstController();
        }
    }
}
