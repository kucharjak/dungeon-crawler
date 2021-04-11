using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.States.EnemyStates
{
    public class FollowState : State<EnemyCharacter>
    {
        [Export] public int FollowDistance = 25;
        
        private Vector2 _preFollowPosition = Vector2.Zero;
        
        public FollowState(EnemyCharacter enemyCharacter) : base(enemyCharacter)
        {
        }
        
        public override void Init()
        {
            _preFollowPosition = Node.Position;
            Node.AnimationPlayer.Play("Run");
        }

        public override void Run(float delta)
        {
            if (Node.Target is null)
            {
                // finish follow state and return to last position before following target
                Node.PopState();
                Node.PushState(new ReturnState(Node, _preFollowPosition));
                
                return;
            }

            var targetDirection = Node.Target.Position - Node.Position;
            targetDirection = targetDirection.Normalized();

            Node.CharacterSprite.FlipH = targetDirection.x < 0;

            var targetDistance = Node.Target.Position.DistanceTo(Node.Position);
            if (targetDistance < FollowDistance)
            {
                Node.PushState(new SingleAttackState(Node));
                return;
            }
            
            Node.MoveAndSlide(targetDirection * Node.MaxSpeed);
        }

        public override void Resume()
        {
            Node.AnimationPlayer.Play("Run");
        }
    }
}