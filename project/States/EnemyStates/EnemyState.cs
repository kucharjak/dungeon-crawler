using DungeonCrawler.AutoLoad;
using DungeonCrawler.Enemies;

namespace DungeonCrawler.States.EnemyStates
{
    public abstract class EnemyState : IState
    {
        protected readonly Enemy Enemy;

        protected EnemyState(Enemy enemy)
        {
            Enemy = enemy;
        }

        public virtual void Init()
        {
            LoggingComponent.Logger.Debug($"{GetType().Name} - Called Init method");
        }

        public virtual void Run(float delta)
        {
            // LoggingComponent.Logger.Debug($"{GetType().Name} - Called Run method");
        }

        public virtual void Resume()
        {
            LoggingComponent.Logger.Debug($"{GetType().Name} - Called Init method");
        }

        public virtual void Pause()
        {
            LoggingComponent.Logger.Debug($"{GetType().Name} - Called Pause method");
        }

        public virtual void End()
        {
            LoggingComponent.Logger.Debug($"{GetType().Name} - Called End method");
        }
    }
}