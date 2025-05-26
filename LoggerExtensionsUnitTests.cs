using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
using LoggerExtensionsNamespace; // Replace with your actual namespace

public class LoggerExtensionsTests
{
    private readonly Mock<ILogger> _mockLogger;

    public LoggerExtensionsTests()
    {
        _mockLogger = new Mock<ILogger>();
    }

    [Fact]
    public void LogAppInfo_ShouldLogInformation()
    {
        // Arrange
        string message = "This is a test info message";
        object[] args = new object[] { };

        // Act
        _mockLogger.Object.LogAppInfo(message, args);

        // Assert
        _mockLogger.Verify(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Fact]
    public void LogAppWarning_ShouldLogWarning()
    {
        string message = "This is a test warning message";
        object[] args = new object[] { };

        _mockLogger.Object.LogAppWarning(message, args);

        _mockLogger.Verify(x => x.Log(
            LogLevel.Warning,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Fact]
    public void LogAppDebug_ShouldLogDebug()
    {
        string message = "This is a debug log";
        object[] args = new object[] { };

        _mockLogger.Object.LogAppDebug(message, args);

        _mockLogger.Verify(x => x.Log(
            LogLevel.Debug,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }

    [Fact]
    public void LogAppError_ShouldLogErrorWithException()
    {
        string message = "This is an error log";
        var exception = new InvalidOperationException("Invalid operation");
        object[] args = new object[] { };

        _mockLogger.Object.LogAppError(message, exception, args);

        _mockLogger.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
            exception,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }
}
