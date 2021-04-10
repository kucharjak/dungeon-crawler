using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Controls;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters.Playable
{
    public class Character : KinematicBody2D
    {
        // Exported variables
        [Export] public int MaxSpeed = 120;
        [Export] public int Acceleration = 600;
        [Export] public int Friction = 800;

        // Character variables
        protected Vector2 Velocity;
        protected CharacterState State = CharacterState.Move;

        // Other node dependencies
        protected Sprite CharacterSprite;
        protected AnimationPlayer AnimationPlayer;

        protected IController Controller = new NoController();
    
        public override void _Ready()
        {
            CharacterSprite = this.GetChildNode<Sprite>("CharacterSprite");
            AnimationPlayer = this.GetChildNode<AnimationPlayer>();
        }
        
        public IController GetController() => Controller;

        public void AttachController(IController controller)
        {
            if (controller is null)
            {
                LoggingComponent.Logger.Error($"It's not possible to attach empty controller to object '{GetPath()}'");
                return;
            }

            Controller = controller;
        }

        public bool DetachController()
        {
            if (Controller.GetType() == typeof(NoController))
                return false;

            Controller = new NoController();
            return true;
        }

        public override void _PhysicsProcess(float delta)
        {
            switch (State)
            {
                case CharacterState.Move:
                    ProcessMove(delta);
                    break;
                case CharacterState.Attack:
                    ProcessAttack(delta);
                    break;
                default:
                    throw new NotImplementedException();
            }

            MoveAndSlide(Velocity);
        }

        protected void ProcessMove(float delta)
        {
            var inputVector = Controller.GetInputVector();

            if (inputVector != Vector2.Zero)
            {
                AnimationPlayer.Play("Run");
                Velocity = Velocity.MoveToward(inputVector * MaxSpeed, Acceleration * delta);
            }
            else
            {
                AnimationPlayer.Play("Idle");
                Velocity = Velocity.MoveToward(Vector2.Zero, Friction * delta);
            }
            
            if (Velocity.x != 0)
                CharacterSprite.FlipH = Velocity.x < 0;

            Attack();
        }
        
        protected void Attack()
        {
            if (!Controller.IsAttackPressed())
                return;
            
            Velocity = Vector2.Zero;
            State = CharacterState.Attack;
            AnimationPlayer.Play("Attack");
        }

        protected void ProcessAttack(float delta)
        {
            if (!AnimationPlayer.IsPlaying())
            {
                State = CharacterState.Move;
                return;
            }
        }
    }
}