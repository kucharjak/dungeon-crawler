using DungeonCrawler.AutoLoad;

namespace DungeonCrawler.States
{
    public class NoState : IState
    {
        public void Init()
        {
            LoggingComponent.Logger.Debug($"{nameof(NoState)} - Called Init method");
        }

        public void Run(float delta)
        {
        }

        public void Resume()
        {
            LoggingComponent.Logger.Debug($"{nameof(NoState)} - Called Resume method");
        }

        public void Pause()
        {
            LoggingComponent.Logger.Debug($"{nameof(NoState)} - Called Pause method");
        }

        public void End()
        {
            LoggingComponent.Logger.Debug($"{nameof(NoState)} - Called End method");
        }
    }
}