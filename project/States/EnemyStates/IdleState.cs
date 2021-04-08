using DungeonCrawler.Enemies;
using DungeonCrawler.Enemies.Skeleton;

namespace DungeonCrawler.States.EnemyStates
{
    public class IdleState : EnemyState
    {
        public IdleState(Enemy enemy) : base(enemy)
        {
        }
        
        public override void Init()
        {
            Enemy.AnimationPlayer.Play("Idle");
        }
    
        public override void Run(float delta)
        {
            if (Enemy.Target is null)
                return;
            
            Enemy.PushState(new FollowState(Enemy));
        }

        public override void Resume()
        {
            Enemy.AnimationPlayer.Play("Idle");
        }
    }
}