using DungeonCrawler.AutoLoad;
using Godot;

namespace DungeonCrawler.States
{
    public abstract class State<TNode> : IState
        where TNode : Node
    {
        /// <summary>
        /// Node assigned for the given state. Mainly the node, that will be changed by state logic. 
        /// </summary>
        protected readonly TNode Node;

        protected State(TNode node)
        {
            Node = node;
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
            LoggingComponent.Logger.Debug($"{GetType().Name} - Called Resume method");
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