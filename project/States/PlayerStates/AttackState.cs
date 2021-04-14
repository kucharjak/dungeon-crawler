using DungeonCrawler.Characters.Playable;
using Godot;

namespace DungeonCrawler.States.PlayerStates
{
    public class AttackState : State<PlayerCharacter>
    {
        public AttackState(PlayerCharacter node) : base(node)
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