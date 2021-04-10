using Godot;

namespace DungeonCrawler.Extensions
{
    public static class TimerExtension
    {
        /// <summary>
        /// Restarts timer
        /// </summary>
        public static void Restart(this Timer timer)
        {
            if (!timer.IsStopped())
            {
                timer.Stop();   
            }
            
            timer.Start();
        }
    }
}