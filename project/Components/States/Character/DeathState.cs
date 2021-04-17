namespace DungeonCrawler.Components.States.Character
{
    public class DeathState : State<Characters.Character>
    {
        public DeathState(Characters.Character node) : base(node)
        {
        }

        public override void Init()
        {
            Node.Die();
        }
    }
}