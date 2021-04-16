using DungeonCrawler.Characters.NonPlayable;

namespace DungeonCrawler.States.EnemyStates
{
    public class IdleState : State<EnemyCharacter>
    {
        public IdleState(EnemyCharacter enemyCharacter) : base(enemyCharacter)
        {
        }
    
        public override void Run(float delta)
        {
            if (Node.GetTarget() is null)
                return;
            
            Node.PushState(new FollowState(Node));
        }
    }
}