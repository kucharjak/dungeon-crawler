using DungeonCrawler.Extensions;
using DungeonCrawler.States;
using DungeonCrawler.States.EnemyStates;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable
{
    public class EnemyCharacter : Character
    {
        // Editable variables
        [Export] public bool IsAggressive = true;

        // Other components
        public Node2D Target;

        public override void _Ready()
        {
            base._Ready();
            
            PushState(new IdleState(this));
        }

        public void OnTargetSpotted(Node2D target)
        {
            Target = target;
        }
    
        public void OnTargetLost(Node2D target)
        {
            if (target != Target)
                return;
            
            Target = null;
        }
    }
}