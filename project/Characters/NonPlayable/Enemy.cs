using DungeonCrawler.Extensions;
using DungeonCrawler.States;
using DungeonCrawler.States.EnemyStates;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable
{
    public class Enemy : KinematicBody2D, IStateComponent<EnemyState>
    {
        [Export] public int MaxSpeed = 80;
        [Export] public int Acceleration = 100;
        [Export] public int Friction = 100;

        [Export] public bool IsAggressive = true;

        public Vector2 Velocity = Vector2.Zero;
        
        public Node2D Target;

        public StateComponent State;

        public Sprite CharacterSprite;

        public AnimationPlayer AnimationPlayer;

        public override void _Ready()
        {
            AnimationPlayer = this.GetChildNode<AnimationPlayer>();
            
            State = this.GetChildNode<StateComponent>();
            PushState(new IdleState(this));

            CharacterSprite = this.GetChildNode<Sprite>("CharacterSprite");
        }

        public void OnTargetSpotted(Node2D target)
        {
            Target = target;
        }
    
        public void OnTargetLost(Node2D target)
        {
            if (target != Target)
                return;
            
            Target = null;
        }

        public int Count => State.Count;

        public void PushState(EnemyState newState)
        {
            State.PushState(newState);
        }

        public EnemyState PopState()
        {
            return (EnemyState)State.PopState();
        }

        public EnemyState PeekState()
        {
            return (EnemyState)State.PeekState();
        }
    }
}