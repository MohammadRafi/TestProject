using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Runtime.CompilerServices;

public static class LoggerExtensions
{
    public static void LogInfoWithContext(this ILogger logger, string message, object[] args = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "")
    {
        string className = Path.GetFileNameWithoutExtension(filePath);
        string formattedMessage = $"{className}.{memberName} | {message}";

        logger.LogInformation(formattedMessage, args ?? Array.Empty<object>());
    }

    public static void LogErrorWithContext(this ILogger logger, string message, Exception ex = null, object[] args = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "")
    {
        string className = Path.GetFileNameWithoutExtension(filePath);
        string formattedMessage = $"{className}.{memberName} | {message} ";

        if (ex != null)
            logger.LogError(ex, formattedMessage, args ?? Array.Empty<object>());
        else
            logger.LogError(formattedMessage, args ?? Array.Empty<object>());
    }

    public static void LogWarningWithContext(this ILogger logger, string message, object[] args = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "")
    {
        string className = Path.GetFileNameWithoutExtension(filePath);
        string formattedMessage = $"{className}.{memberName} | {message}";

        logger.LogWarning(formattedMessage, args ?? Array.Empty<object>());
    }
}
