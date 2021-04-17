using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DungeonCrawler.Combat
{
    public sealed class InvincibilityComponent : Node
    {
        // Exported variables
        [Export] public float InvincibilityTimeout = 1f;
        
        // Public properties
        [Signal] public delegate void InvincibilityStarted();
        [Signal] public delegate void InvincibilityEnded();

        // Protected components
        private Timer _invincibilityTimer = new Timer();

        public override void _Ready()
        {
            _invincibilityTimer.Connect("timeout", this, nameof(EndInvincibility));
            AddChild(_invincibilityTimer);
        }

        public void StartInvincibility()
        {
            _invincibilityTimer.Start(InvincibilityTimeout);
            EmitSignal(nameof(InvincibilityStarted));
        }

        public void EndInvincibility()
        {
            _invincibilityTimer.Stop();
            EmitSignal(nameof(InvincibilityEnded));
        }
    }
}