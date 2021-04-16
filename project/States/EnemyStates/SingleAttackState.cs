using DungeonCrawler.Characters.NonPlayable;
using DungeonCrawler.Extensions;

namespace DungeonCrawler.States.EnemyStates
{
    public class SingleAttackState : State<EnemyCharacter>
    {
        public SingleAttackState(EnemyCharacter node) : base(node)
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