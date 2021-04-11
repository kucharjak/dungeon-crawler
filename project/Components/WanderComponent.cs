using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters;
using DungeonCrawler.Characters.NonPlayable;
using DungeonCrawler.Extensions;
using DungeonCrawler.States.EnemyStates;
using Godot;

namespace DungeonCrawler.Components
{
    public class WanderComponent : Timer
    {
        [Export] public int WanderDistance = 100;

        [Export] public bool WanderingEnabled = true;

        private EnemyCharacter _parent;

        public override void _Ready()
        {
            base._Ready();

            _parent = this.GetParentNodeRecurse<EnemyCharacter>();
        }

        public void OnTimeout()
        {
            if (WanderingEnabled == false || _parent.PeekState().GetType() != typeof(IdleState))
            {
                this.Restart();
                return;
            }

            var randomPosition = _parent.StartPosition.GetRandomPositionForDistance(WanderDistance);

            _parent.PushState(new WanderState(_parent, randomPosition, this));
        }
    }
}