using System;
using DungeonCrawler.Logging;
using Godot;

namespace DungeonCrawler.AutoLoad
{
    public class Global : Node
    {
        [Export]
        public LogLevel LoggingLevel { get; set; }
        
        [Export]
        public int RandomSeed { get; set; }
    
        public override void _Ready()
        {
            LoggingComponent.Logger = new GodotLogger(LoggingLevel);
            RandomGenerator.Random = new Random(RandomSeed);
        }
    }
}
