namespace Shezzy.Shared.Abstractions
{
    public interface ISLogger
    {
        void LogAndThrowArgumentNullException(string message);
    }
}
