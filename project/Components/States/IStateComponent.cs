namespace DungeonCrawler.Components.States
{
    public interface IStateComponent<TState>
        where TState : IState
    {
        int StatesCount { get; }
        
        void PushState(TState newState);

        TState PopState();

        TState PeekState();
    }
}