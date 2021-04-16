using System.Configuration;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Components;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable.Skeleton
{
    public sealed class Skeleton : EnemyCharacter
    {
        public override void _Ready()
        {
            base._Ready();
            
            // Random scale of skeleton for variety 
            var randomScale = (float)RandomGenerator.Random.Next(90, 120);
            randomScale /= 100;

            Scale *= randomScale;
        }
    }
}
