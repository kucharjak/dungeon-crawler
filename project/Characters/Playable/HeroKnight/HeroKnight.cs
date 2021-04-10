using DungeonCrawler.Controls;

namespace DungeonCrawler.Characters.Playable.HeroKnight
{
    public class HeroKnight : PlayerCharacter
    {
        public override void _Ready()
        {
            base._Ready();
            
            Controller = new FirstController();
        }
    }
}
