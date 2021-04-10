namespace DungeonCrawler.States
{
    public interface IStateComponent<TState>
        where TState : IState
    {
        int Count { get; }
        
        void PushState(TState newState);

        TState PopState();

        TState PeekState();
    }
}