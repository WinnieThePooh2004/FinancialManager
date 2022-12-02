using Microsoft.Extensions.Logging;

namespace FinancialManager.Shared.Extentions
{
    public static class ILoggerExtentions
    {
        public static void LogAndThrow(this ILogger logger, Exception exception)
        {
            logger.LogError(exception, "{message}", exception.Message);
            throw exception;
        }

        public static void LogAndThrow(this ILogger logger, string message, Exception exception)
        {
            logger.LogError(exception, "{message}", message);
            throw exception;
        }
    }
}
