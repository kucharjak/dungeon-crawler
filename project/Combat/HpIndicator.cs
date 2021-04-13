using System;
using DungeonCrawler.AutoLoad;
using DungeonCrawler.Characters;
using DungeonCrawler.Extensions;
using Godot;

namespace DungeonCrawler.Combat
{
    public class HpIndicator : Node2D
    {
        private ColorRect _baseColor;
        private ColorRect _healthColor;
        private ColorRect _lossHealthColor;
        private AnimationPlayer _animationPlayer;
        private Character _character;
        
        public override void _Ready()
        {
            _baseColor = this.GetChildNode<ColorRect>("BaseColor");
            _healthColor = this.GetChildNode<ColorRect>("HealthColor");
            _lossHealthColor = this.GetChildNode<ColorRect>("LoseHealthColor");
            _animationPlayer = this.GetChildNode<AnimationPlayer>();
            
            _character = this.GetParentNodeRecurse<Character>();
        }

        public void OnChangeHp(int oldValue, int newValue)
        {
            if (newValue > oldValue)
                return;
            
            UpdateHealthIndication();
            
            var width = _baseColor.RectSize.x;
            var stats = _character.Stats;
            
            var lossValue = oldValue - newValue;
            var lossWidth = width * (lossValue / (float) stats.MaxHp);
            
            _lossHealthColor.RectSize = new Vector2(lossWidth, _lossHealthColor.RectSize.y);
            _lossHealthColor.RectPosition = new Vector2(_healthColor.RectSize.x, _lossHealthColor.RectPosition.y);
            _animationPlayer.Play("Blink");
        }

        public void OnChangeMaxHP(int oldValue, int newValue)
        {
            LoggingComponent.Logger.Debug($"MAX HP was changed {oldValue} > {newValue}");
            UpdateHealthIndication();
        }

        public void UpdateHealthIndication()
        {
            var width = _baseColor.RectSize.x;
            var stats = _character.Stats;
            
            var healthWidth = width * (stats.Hp / (float) stats.MaxHp);

            _healthColor.RectSize = new Vector2(healthWidth, _healthColor.RectSize.y);
        }
    }
}
