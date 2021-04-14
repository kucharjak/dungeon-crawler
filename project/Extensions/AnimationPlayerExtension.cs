using Godot;

namespace DungeonCrawler.Extensions
{
    public static class AnimationPlayerExtension
    {
        /// <summary>
        /// Pauses actual animation so it could be resumed later.
        /// </summary>
        public static void Pause(this AnimationPlayer animationPlayer)
        {
            animationPlayer.Stop(false);
        }

        /// <summary>
        /// Resumes actual animation. 
        /// </summary>
        public static void Resume(this AnimationPlayer animationPlayer)
        {
            animationPlayer.Play();
        }
        
        /// <summary>
        /// Jumps to the first frame of actual animation.
        /// </summary>
        /// <param name="animationPlayer"></param>
        public static void ToStart(this AnimationPlayer animationPlayer)
        {
            animationPlayer.Seek(0f, true);
        }

        /// <summary>
        /// Jumps to the last frame of actual animation.
        /// </summary>
        public static void ToEnd(this AnimationPlayer animationPlayer)
        {
            animationPlayer.Seek(animationPlayer.CurrentAnimationLength, true);
        }
    }
}