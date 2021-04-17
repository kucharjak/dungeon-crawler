namespace DungeonCrawler.Logging
{
    public abstract class Logger : ILogger
    {
        protected LogLevel LogLevel { get; }
        
        protected Logger(LogLevel logLevel)
        {
            LogLevel = logLevel;
        }
        
        public void Log(LogLevel level, string message)
        {
            if (level < LogLevel)
                return;
            
            InternalLog(level, message);
        }

        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void Warn(string message)
        {
            Log(LogLevel.Warn, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        protected abstract void InternalLog(LogLevel level, string message);
    }
}