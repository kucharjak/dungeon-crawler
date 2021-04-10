using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters.NonPlayable;

namespace DungeonCrawler.States.EnemyStates
{
    public class FollowState : State<EnemyCharacter>
    {
        public FollowState(EnemyCharacter enemyCharacter) : base(enemyCharacter)
        {
        }
        
        public override void Init()
        {
            Node.AnimationPlayer.Play("Run");
        }

        public override void Run(float delta)
        {
            if (Node.Target is null)
            {
                Node.PopState();
                return;
            }

            var targetDirection = Node.Target.Position - Node.Position;
            targetDirection = targetDirection.Normalized();

            Node.CharacterSprite.FlipH = targetDirection.x < 0;

            Node.MoveAndSlide(targetDirection * Node.MaxSpeed);
        }

        public override void Resume()
        {
            Node.AnimationPlayer.Play("Run");
        }
    }
}