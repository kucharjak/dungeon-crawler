using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Dungeons
{
    public class Box : StaticBody2D
    {
        private AnimationPlayer _animationPlayer;
        private string _baseAnimation;
    
        public override void _Ready()
        {
            _animationPlayer = this.GetChildNode<AnimationPlayer>();

            var animationList = _animationPlayer.GetAnimationList();
            if (animationList.Length == 0)
            {
                LoggingComponent.Logger.Warn($"Scene '{nameof(Box)}' doesn't have any animations");
                return;
            }

            var randIndex = RandomGenerator.Random.Next(0, _animationPlayer.GetAnimationList().Length);
            _baseAnimation = animationList[randIndex];
            _animationPlayer.Play(_baseAnimation);
        }
    }
}
