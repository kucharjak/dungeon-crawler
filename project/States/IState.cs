namespace DungeonCrawler.States
{
    public interface IState
    {
        void Init();

        void Run(float delta);

        void Resume();
        
        void Pause();

        void End();
    }
}