using System;
using System.Configuration;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Components;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Characters.NonPlayable.Skeleton
{
    public sealed class Skeleton : EnemyCharacter
    {
        private float _walkSpeedLimitSquared = 0; 
            
        public override void _Ready()
        {
            base._Ready();
            
            // Random scale of skeleton for variety 
            var randomScale = (float)RandomGenerator.Random.Next(90, 120);
            randomScale /= 100;

            Scale *= randomScale;
            
            _walkSpeedLimitSquared = (float)Math.Pow(MaxSpeed * 0.6f, 2);
        }
        
        public override Vector2 ApplyVelocity(Vector2 velocity)
        {
            Velocity = velocity;
            
            var realVelocity = MoveAndSlide(Velocity);
            
            // TODO : Godot documentation recommends using of LenghtSquared when comparing vectors for performance reasons - checkout difference.  
            var realVelocityLen = realVelocity.LengthSquared();
            
            if (realVelocityLen > _walkSpeedLimitSquared)
            {
                AnimationPlayer.Play("Run");
            }
            else if (realVelocityLen <= float.Epsilon)
            {
                AnimationPlayer.Play("Idle");
            }
            else
            {
                AnimationPlayer.Play("Walk");
            }
            
            if (Velocity.x != 0)
            {
                var flip = velocity.x < 0;
            
                CharacterSprite.FlipH = flip;
                CharacterHitBox.Scale = new Vector2(flip ? -1 : 1, CharacterHitBox.Scale.y);
            }

            return realVelocity;
        }
    }
}
