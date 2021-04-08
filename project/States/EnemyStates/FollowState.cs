using DungeonCrawler.AutoLoad;
using DungeonCrawler.Enemies;
using DungeonCrawler.Enemies.Skeleton;

namespace DungeonCrawler.States.EnemyStates
{
    public class FollowState : EnemyState
    {
        public FollowState(Enemy enemy) : base(enemy)
        {
        }
        
        public override void Init()
        {
            Enemy.AnimationPlayer.Play("Run");
        }

        public override void Run(float delta)
        {
            if (Enemy.Target is null)
            {
                Enemy.PopState();
                return;
            }

            var targetDirection = Enemy.Target.Position - Enemy.Position;
            targetDirection = targetDirection.Normalized();

            Enemy.CharacterSprite.FlipH = targetDirection.x < 0;

            Enemy.MoveAndSlide(targetDirection * Enemy.MaxSpeed);
        }

        public override void Resume()
        {
            Enemy.AnimationPlayer.Play("Run");
        }
    }
}