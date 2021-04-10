using System.Collections.Generic;
using Godot;

namespace DungeonCrawler.States
{
    public class StateComponent : Node, IStateComponent<IState>
    {
        protected readonly Stack<IState> States;

        public StateComponent()
        {
            States = new Stack<IState>();
        }
        
        public override void _PhysicsProcess(float delta)
        {
            var actualState = PeekState();
            actualState.Run(delta);
        }
        
        public virtual int Count => States.Count;

        public virtual void PushState(IState newState)
        {
            var actualState = PeekState();
            actualState.Pause();
            
            newState.Init();
            States.Push(newState);
        }

        public virtual IState PopState()
        {
            if (States.Count == 0)
                return new NoState();
            
            var actualState = States.Pop();
            actualState.End();

            var resumedState = PeekState();
            resumedState.Resume();

            return actualState;
        }

        public virtual IState PeekState()
        {
            if (States.Count == 0)
                return new NoState();
            
            return States.Peek();
        }
    }
}
