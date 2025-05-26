public static void LogAppDebug(this ILogger logger, string message, object[] args = null,
    [CallerMemberName] string methodName = "",
    [CallerFilePath] string filePath = "")
{
    string className = Path.GetFileNameWithoutExtension(filePath);
    string fullContext = $"{className}.{methodName} | {message}";
    logger.LogDebug(fullContext, args ?? Array.Empty<object>());
}
