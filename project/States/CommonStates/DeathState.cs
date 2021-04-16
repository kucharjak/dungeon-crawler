using DungeonCrawler.Characters;
using Godot;

namespace DungeonCrawler.States.CommonStates
{
    public class DeathState : State<Character>
    {
        public DeathState(Character node) : base(node)
        {
        }

        public override void Init()
        {
            Node.Die();
        }
    }
}