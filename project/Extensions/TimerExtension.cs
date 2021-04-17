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
        
        /// <summary>
        /// Restarts timer with given timeout
        /// </summary>
        public static void Restart(this Timer timer, float millis)
        {
            if (!timer.IsStopped())
            {
                timer.Stop();   
            }
            
            timer.Start(millis);
        }
    }
}