using DungeonCrawler.Controls;
using Godot;

namespace DungeonCrawler.Characters.HeroKnight
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
