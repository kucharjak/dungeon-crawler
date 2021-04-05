using DungeonCrawler.Characters;
using Godot;

namespace DungeonCrawler.Combat
{
    public class DetectionZone : Area2D
    {
        [Signal] public delegate void TargetSpotted(Node2D target);
        [Signal] public delegate void TargetLost(Node2D target);

        public void OnBodyEntered(Node2D body)
        {
            EmitSignal(nameof(TargetSpotted), body);
        }
        
        public void OnBodyExited(Node2D body)
        {
            EmitSignal(nameof(TargetLost), body);
        }
    }
}
