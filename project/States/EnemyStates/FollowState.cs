using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.States.EnemyStates
{
    public class FollowState : State<EnemyCharacter>
    {
        [Export] public int FollowDistance = 150;
        [Export] public int AttackDistance = 25;
        
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
            
            Node.Velocity = Node.Velocity.MoveToward(targetDirection * Node.MaxSpeed, Node.Acceleration * delta);

            if (Node.Velocity.x != 0)
            {
                var flip = Node.Velocity.x < 0;

                Node.CharacterSprite.FlipH = flip;
                Node.CharacterHitBox.Scale = new Vector2(flip ? -1 : 1, Node.CharacterHitBox.Scale.y);
            }

            var targetDistance = Node.Target.Position.DistanceTo(Node.Position);
            if (targetDistance >= FollowDistance)
            {
                // finish follow state and return to last position before following target
                Node.Target = null;
                Node.PopState();
                Node.PushState(new ReturnState(Node, _preFollowPosition));
                
                return;
            }
            
            if (targetDistance < AttackDistance)
            {
                Node.Velocity = Vector2.Zero;
                Node.PushState(new SingleAttackState(Node));
                return;
            }
            
            Node.MoveAndSlide(Node.Velocity);
        }

        public override void Resume()
        {
            Node.AnimationPlayer.Play("Run");
        }
    }
}
