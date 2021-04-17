using DungeonCrawler.Characters.Playable;

namespace DungeonCrawler.Components.States.Character.Player
{
    public class AttackState : State<PlayerCharacter>
    {
        public AttackState(PlayerCharacter node) : base(node)
        {
        }

        public override void Init()
        {
            Node.Attack();
        }

        public override void Run(float delta)
        {
            if (Node.IsAttacking)
                return;
            
            Node.PopState();
        }
    }
}