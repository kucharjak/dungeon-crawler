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
            Node.AnimationPlayer.Play("Attack");
        }

        public override void Run(float delta)
        {
            if (Node.AnimationPlayer.IsPlaying())
                return;
            
            Node.PopState();
        }

        public override void Pause()
        {
            Node.AnimationPlayer.ToStart();
            Node.AnimationPlayer.Stop();
        }

        public override void End()
        {
            Node.AnimationPlayer.ToStart();
            Node.AnimationPlayer.Stop();
        }
    }
}