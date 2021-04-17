using System;
using DungeonCrawler.AutoLoad;
using Godot;

namespace DungeonCrawler.Extensions
{
    public static class VectorExtension
    {
        public static Vector2 GetRandomPositionForDistance(this Vector2 vector2, int distance)
        {
            var absDistance = Math.Abs(distance);

            var x = RandomGenerator.Random.Next(-1 * absDistance, absDistance);
            var y = RandomGenerator.Random.Next(-1 * absDistance, absDistance);

            return vector2 + new Vector2(x, y);
        }
    }
}