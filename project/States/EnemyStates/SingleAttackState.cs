using DungeonCrawler.Characters.NonPlayable;

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
    }
}