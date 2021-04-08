using DungeonCrawler.Enemies;

namespace DungeonCrawler.States
{
    public interface IStateComponent<TState>
        where TState : IState
    {
        void PushState(TState newState);

        TState PopState();

        TState PeekState();
    }
}