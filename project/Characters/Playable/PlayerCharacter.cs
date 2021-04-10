using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Controls;
using DungeonCrawler.Extensions;
using DungeonCrawler.States.PlayerStates;
using Godot;

namespace DungeonCrawler.Characters.Playable
{
    public class PlayerCharacter : Character
    {
        protected IController Controller = new NoController();
        
        public override void _Ready()
        {
            base._Ready();
            
            PushState(new MoveState(this));
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
    }
}