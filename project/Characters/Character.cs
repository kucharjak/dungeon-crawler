using DungeonCrawler.Extensions;
using DungeonCrawler.States;
using Godot;

namespace DungeonCrawler.Characters
{
    public abstract class Character : KinematicBody2D, IStateComponent<IState>
    {
        // Editable variables
        [Export] public int MaxSpeed = 120;
        [Export] public int Acceleration = 600;
        [Export] public int Friction = 800;
        
        // Character variables
        internal Vector2 Velocity = Vector2.Zero;
        
        // Other components
        internal Sprite CharacterSprite;
        internal AnimationPlayer AnimationPlayer;
        internal StateComponent State;
        
        public override void _Ready()
        {
            CharacterSprite = this.GetChildNode<Sprite>("CharacterSprite");
            AnimationPlayer = this.GetChildNode<AnimationPlayer>();
            State = this.GetChildNode<StateComponent>();
        }

        public int Count => State.Count;
        
        public void PushState(IState newState)
        {
            State.PushState(newState);
        }

        public IState PopState()
        {
            return State.PopState();
        }

        public IState PeekState()
        {
            return State.PeekState();
        }
    }
}