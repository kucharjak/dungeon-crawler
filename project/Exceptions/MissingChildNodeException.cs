using System;

namespace DungeonCrawler.Exceptions
{
    public class MissingChildNodeException : Exception
    {
        public MissingChildNodeException() : base()
        {
        }

        public MissingChildNodeException(string message) : base(message)
        {
        }

        public MissingChildNodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}