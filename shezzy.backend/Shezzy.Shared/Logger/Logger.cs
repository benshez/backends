using Shezzy.Shared.Abstractions;

namespace Shezzy.Shared.Logger
{
    public class Logger : ISLogger
    {
        public void LogAndThrowArgumentNullException(string message)
        {
            Serilog.Log.Error(message);
            throw new ArgumentNullException(message);
        }
    }
}

