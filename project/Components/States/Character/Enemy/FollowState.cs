using DungeonCrawler.Characters.NonPlayable;
using Godot;

namespace DungeonCrawler.Components.States.Character.Enemy
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
        }

        public override void Run(float delta)
        {
            var target = Node.GetTarget(); 
            if (target is null)
            {
                // finish follow state and return to last position before following target
                Node.PopState();
                Node.PushState(new ReturnState(Node, _preFollowPosition));
                
                return;
            }

            var targetDirection = target.Position - Node.Position;
            targetDirection = targetDirection.Normalized();
            
            var newVelocity = Node.Velocity.MoveToward(targetDirection * Node.MaxSpeed, Node.Acceleration * delta);

            var targetDistance = target.Position.DistanceTo(Node.Position);
            if (targetDistance >= FollowDistance)
            {
                // finish follow state and return to last position before following target
                Node.OnTargetLost(target);
                Node.PopState();
                Node.PushState(new ReturnState(Node, _preFollowPosition));
                
                return;
            }
            
            if (targetDistance < AttackDistance)
            {
                Node.ApplyVelocity(Vector2.Zero);
                Node.PushState(new SingleAttackState(Node));
                return;
            }

            Node.ApplyVelocity(newVelocity);
        }
    }
}
