using System;
using Godot;

namespace DungeonCrawler.Logging
{
    public class GodotLogger : Logger
    {
        public GodotLogger(LogLevel logLevel) : base(logLevel)
        {
        }

        protected override void InternalLog(LogLevel level, string message)
        {
            var printedLog = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - [{level.ToString()}]: {message}";

            switch (level)
            {
                default:
                    GD.Print(printedLog);
                    break;
                case LogLevel.Error:
                    GD.PrintErr(printedLog);
                    break;
            }
        }
    }
}